using DddInPractice.CommandHandlers.Results;
using MediatR;

namespace DddInPractice.Commands.Commands
{
    public class BuySnackCommand : IRequest<CommandResult>
    {
        public int SnackMachineId { get; set; }
        public int SlotPosition { get; set; }


        public int OneCentCount { get; set; }
        public int TenCentCount { get; set; }
        public int QuarterCount { get; set; }
        public int OneDollarCount { get; set; }
        public int FiveDollarCount { get; set; }

        public string UserId { get; set; }
    }
}
