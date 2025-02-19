using CodeD.Domain.Entities;

namespace CodeD.Domain.Interfaces;

public interface IEntityRepository<TEntity, TId> : IEntityRepository
    where TEntity : Entity<TId>
{
    ValueTask<TEntity?> GetByIdAsync(TId id);

    Task<TEntity?> GetByKeyAsync(string key);

    Task<List<TEntity>> ListAsync();

    TEntity Update(TEntity entity);

    TEntity Insert(TEntity entity);

    void Delete(TEntity entity);
}