using System.Threading;
using System.Threading.Tasks;
using DddInPractice.CommandHandlers.Commands;
using DddInPractice.CommandHandlers.Results;
using DddInPractice.Domain.Aggregates.SnakMachineAggregate;
using MediatR;

namespace DddInPractice.CommandHandlers.Handlers
{
    public class AddSnackMachineSlotCommandHandler :
        IRequestHandler<AddSnackMachineSlotCommand, CommandResult>
    {
        private readonly ISnakMachineRepository _snackMachineRepository;

        public AddSnackMachineSlotCommandHandler(
            ISnakMachineRepository snackMachineRepository)
        {
            _snackMachineRepository = snackMachineRepository;
        }

        public async Task<CommandResult> Handle(AddSnackMachineSlotCommand request, 
            CancellationToken cancellationToken)
        {
            var snackMachine = await _snackMachineRepository
                .GetAsync(request.SnackMachineId);
            if(snackMachine == null)
            {
                return CommandResult.GetFailed("Snack Machine instance is not found.");
            }

            if (snackMachine.CanAddSlot())
            {
                snackMachine.AddSlot(request.Position, request.ItemsQuantity,
                            request.ItemPrice, request.SnackTypeId);
                await _snackMachineRepository.UnitOfWork.SaveEntitiesAsync();

                return CommandResult.GetSuccess();
            }
            else
            {
                return CommandResult.GetFailed("No available place for slot");
            }            
        }
    }
}
