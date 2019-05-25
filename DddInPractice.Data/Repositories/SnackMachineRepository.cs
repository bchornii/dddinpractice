using DddInPractice.Domain.Aggregates.SnakMachineAggregate;
using DddInPractice.Domain.SeedObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DddInPractice.Data.Repositories
{
    public class SnackMachineRepository : ISnakMachineRepository
    {
        private readonly DefaultDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public SnackMachineRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public SnackMachine Add(SnackMachine snackMachine)
        {
            return _context.SnackMachines.Add(snackMachine).Entity;
        }

        public Task<SnackMachine> GetAsync(int snackMachineId)
        {
            return _context.SnackMachines
                .Include(m => m.Slots)
                    .ThenInclude(s => s.SnakType)
                .Where(m => m.Id == snackMachineId)
                .SingleOrDefaultAsync();
        }

        public void Update(SnackMachine snackMachine)
        {
            _context.Entry(snackMachine).State = EntityState.Modified;
        }
    }
}
