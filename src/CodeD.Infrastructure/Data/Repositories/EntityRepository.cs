using CodeD.Domain.Entities;
using CodeD.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeD.Infrastructure.Data.Repositories;

public class EntityRepository<TEntity, TId>(CodeDDbContext _codeDDbContext) : IEntityRepository<TEntity, TId>
    where TEntity : Entity<TId>, new()
{
    public ValueTask<TEntity?> GetByIdAsync(TId id) => _codeDDbContext.Set<TEntity>().FindAsync(id);

    public Task<TEntity?> GetByKeyAsync(string key) => _codeDDbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Key == key);

    public Task<List<TEntity>> ListAsync() => _codeDDbContext.Set<TEntity>().ToListAsync();

    public TEntity Insert(TEntity entity)
    {
        _codeDDbContext.Add(entity);
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        _codeDDbContext.Update(entity);
        return entity;
    }

    public void Delete(TEntity entity)
    {
        _codeDDbContext.Remove(entity);
    }
}