using CodeD.Domain.Categories;

namespace CodeD.Infrastructure.Data.Repositories;

public class CategoryRepository(CodeDDbContext _codeDDbContext)
    : EntityRepository<Category, CategoryId>(_codeDDbContext), ICategoryRepository
{
    private readonly CodeDDbContext _codeDDbContext = _codeDDbContext;

    public async Task AddAsync(Category entity)
    {
        await _codeDDbContext.AddAsync(entity);
    }
}
