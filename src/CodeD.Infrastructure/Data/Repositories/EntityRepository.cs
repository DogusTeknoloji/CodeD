using CodeD.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CodeD.Infrastructure.Data.Repositories;

public class EntityRepository<TEntity, TId>(CodeDDbContext _codeDDbContext) : IEntityRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : class, IEntityId
{
    public ValueTask<TEntity?> GetByIdAsync(TId id) => _codeDDbContext.Set<TEntity>().FindAsync(id);

    public Task<TEntity?> GetByKeyAsync(Key key) => _codeDDbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Key == key);

    public Task<List<TEntity>> ListAsync() => _codeDDbContext.Set<TEntity>().ToListAsync();
}