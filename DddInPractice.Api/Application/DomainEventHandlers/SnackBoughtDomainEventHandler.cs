using DddInPractice.Domain.Aggregates.SnakMachineAggregate.DomainEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DddInPractice.Api.Application.DomainEventHandlers
{
    public class SnackBoughtDomainEventHandler :
        INotificationHandler<SnackBoughtDomainEvent>
    {
        public Task Handle(SnackBoughtDomainEvent notification, 
            CancellationToken cancellationToken)
        {
            // TODO: additional actions as
            //          1) updating other aggregates (inject repo
            //             and other stuff here and read other aggr,
            //             update it and then save via SaveChangesAsync()
            //          2) propagation integration events or adding 
            //             such an event to some storage only and 
            //             propagation can take place at SaveChangesAsync()
            //             after changes successfully saved to db
            //          3) any other actions needed to business

            return Task.CompletedTask;
        }
    }
}
