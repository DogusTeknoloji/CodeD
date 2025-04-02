using CodeD.Domain.Abstractions;

namespace CodeD.Domain.Interfaces;

public interface IEntityRepository<TEntity, TID> : IEntityRepository
    where TEntity : Entity<TID>
    where TID : class, IEntityId
{
    ValueTask<TEntity?> GetByIdAsync(TID id);

    Task<TEntity?> GetByKeyAsync(string key);

    Task<List<TEntity>> ListAsync();

    TEntity Update(TEntity entity);

    TEntity Insert(TEntity entity);

    void Delete(TEntity entity);
}