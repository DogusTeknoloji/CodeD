namespace CodeD.Domain.Abstractions;

public interface IEntityRepository;

public interface IEntityRepository<TEntity, TId> : IEntityRepository
    where TEntity : Entity<TId>
    where TId : class, IEntityId
{
    ValueTask<TEntity?> GetByIdAsync(TId id);

    Task<TEntity?> GetByKeyAsync(Key key);

    Task<List<TEntity>> ListAsync();
}