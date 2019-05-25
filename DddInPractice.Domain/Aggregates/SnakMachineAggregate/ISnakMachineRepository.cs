using DddInPractice.Domain.SeedObjects;
using System.Threading.Tasks;

namespace DddInPractice.Domain.Aggregates.SnakMachineAggregate
{
    public interface ISnakMachineRepository : IRepository<SnackMachine>
    {
        SnackMachine Add(SnackMachine snackMachine);

        void Update(SnackMachine snackMachine);

        Task<SnackMachine> GetAsync(int snackMachineId);
    }
}
