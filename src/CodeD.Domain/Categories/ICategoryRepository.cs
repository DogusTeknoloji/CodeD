using CodeD.Domain.Abstractions;

namespace CodeD.Domain.Categories;

public interface ICategoryRepository : IEntityRepository<Category, CategoryId>
{
    Task AddAsync(Category entity);
}
