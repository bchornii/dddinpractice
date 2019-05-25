using System.Threading;
using System.Threading.Tasks;
using DddInPractice.CommandHandlers.Results;
using DddInPractice.Commands.Commands;
using DddInPractice.Domain.Aggregates.SnakMachineAggregate;
using MediatR;

namespace DddInPractice.Commands.Handlers
{
    public class BuySnackCommandHandler :
        IRequestHandler<BuySnackCommand, CommandResult>
    {
        private readonly ISnakMachineRepository _snackMachineRepository;

        public BuySnackCommandHandler(
            ISnakMachineRepository snackMachineRepository)
        {
            _snackMachineRepository = snackMachineRepository;
        }

        public async Task<CommandResult> Handle(BuySnackCommand request, 
            CancellationToken cancellationToken)
        {
            var snackMachine = await _snackMachineRepository
                .GetAsync(request.SnackMachineId);
            if (snackMachine == null)
            {
                return CommandResult.GetFailed("Snack Machine instance is not found.");
            }

            var moneyInTran = new Money(request.OneCentCount, request.TenCentCount,
             request.QuarterCount, request.OneDollarCount, request.FiveDollarCount);

            snackMachine.InsertMoney(moneyInTran);
            if (snackMachine.CanBuySnak(request.SlotPosition))
            {
                snackMachine.BuySnack(request.SlotPosition);
                await _snackMachineRepository.UnitOfWork.SaveEntitiesAsync();
            }
            else
            {                
                return CommandResult.GetFailed("Cannot buy snack.");
            }

            return CommandResult.GetSuccess();
        }
    }
}
