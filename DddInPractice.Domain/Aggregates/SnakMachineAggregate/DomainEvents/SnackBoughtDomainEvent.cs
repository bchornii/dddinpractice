using MediatR;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate.DomainEvents
{
    public class SnackBoughtDomainEvent : INotification
    {                
        public int SnackTypeId { get; }
        public decimal SnackPrice { get; }

        public SnackBoughtDomainEvent(int snackTypeId, decimal snackPrice)
        {            
            SnackTypeId = snackTypeId;
            SnackPrice = snackPrice;
        }
    }
}
