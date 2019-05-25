using DddInPractice.CommandHandlers.Results;
using MediatR;

namespace DddInPractice.CommandHandlers.Commands
{
    public class AddSnackMachineSlotCommand : IRequest<CommandResult>
    {
        public int SnackMachineId { get; set; }

        public int Position { get; set; }
        public int ItemsQuantity { get; set; }
        public decimal ItemPrice { get; set; }
        public int SnackTypeId { get; set; }

        public string UserId { get; set; }
    }
}
