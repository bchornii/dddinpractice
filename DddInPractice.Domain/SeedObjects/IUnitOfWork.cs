using System.Threading;
using System.Threading.Tasks;

namespace DddInPractice.Domain.SeedObjects
{
    public interface IUnitOfWork
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}
