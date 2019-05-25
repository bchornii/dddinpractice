namespace DddInPractice.Domain.SeedObjects
{
    public interface IRepository<T> where T : AggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
