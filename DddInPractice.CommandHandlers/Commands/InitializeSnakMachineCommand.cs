using DddInPractice.CommandHandlers.Results;
using MediatR;

namespace DddInPractice.CommandHandlers.Commands
{
    public class InitializeSnakMachineCommand : IRequest<CommandResult>
    {
        public int OneCentCount { get; set; }
        public int TenCentCount { get; set; }
        public int QuarterCount { get; set; }
        public int OneDollarCount { get; set; }
        public int FiveDollarCount { get; set; }

        public string UserId { get; set; }
    }
}
