using DddInPractice.Data.Configurations;
using DddInPractice.Domain.Aggregates.SnakMachineAggregate;
using DddInPractice.Domain.SeedObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DddInPractice.Data
{
    public class DefaultDbContext : DbContext, IUnitOfWork
    {
        public DbSet<SnackMachine> SnackMachines { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<SnakType> SnakTypes { get; set; }

        private IMediator _mediator;
        private IDbContextTransaction _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;

        public DefaultDbContext(DbContextOptions<DefaultDbContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SnakMachineEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SlotEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SnakTypeEntityTypeConfiguration());
        }

        // TODO: saving Owned props have some odds
        // for details: https://msdn.microsoft.com/en-us/magazine/mt846463.aspx
        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added ||
                    entry.State == EntityState.Modified ||
                    IsOwnedEntitiesStateChanged(entry))
                {
                    if(entry.Properties.Any(p => p.Metadata.Name == "LastUpdated"))
                    {
                        entry.Property("LastUpdated").CurrentValue = DateTime.UtcNow;
                    }                    
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        private bool IsOwnedEntitiesStateChanged(EntityEntry entityEntry)
        {
            if(entityEntry.Entity is SnackMachine)
            {
                var moneyInsideState = entityEntry.Reference(nameof(
                    SnackMachine.MoneyInside)).TargetEntry.State;

                return EntityStateChanged(moneyInsideState);
            }
            return false;
        }

        public bool EntityStateChanged(EntityState entityState) =>
            entityState == EntityState.Added || 
            entityState == EntityState.Modified || 
            entityState == EntityState.Deleted;

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // TODO: dispatch domain events if any HERE

            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            await SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction != _currentTransaction)
            {
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
            }

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
