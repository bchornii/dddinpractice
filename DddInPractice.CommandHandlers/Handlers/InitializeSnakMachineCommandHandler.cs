using System.Threading;
using System.Threading.Tasks;
using DddInPractice.CommandHandlers.Commands;
using DddInPractice.CommandHandlers.Results;
using DddInPractice.Domain.Aggregates.SnakMachineAggregate;
using MediatR;

namespace DddInPractice.CommandHandlers.Handlers
{
    public class InitializeSnakMachineCommandHandler :
        IRequestHandler<InitializeSnakMachineCommand, CommandResult>
    {
        private readonly ISnakMachineRepository _snakMachineRepository;

        public InitializeSnakMachineCommandHandler(ISnakMachineRepository snakMachineRepository)
        {
            _snakMachineRepository = snakMachineRepository;
        }

        public async Task<CommandResult> Handle(InitializeSnakMachineCommand request, 
            CancellationToken cancellationToken)
        {
            var moneyInside = new Money(request.OneCentCount, request.TenCentCount,
                request.QuarterCount, request.OneDollarCount, request.FiveDollarCount);

            var snackMachine = new SnackMachine(moneyInside);
            _snakMachineRepository.Add(snackMachine);

            await _snakMachineRepository
                .UnitOfWork.SaveEntitiesAsync();

            return CommandResult.GetSuccess();
        }
    }
}
