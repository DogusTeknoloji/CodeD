using CodeD.Domain.Entities;

namespace CodeD.Domain.Interfaces;

public interface IProductRepository : IEntityRepository<Product, int>
{
    Task<Product> GetByNameAsync(string name);
}