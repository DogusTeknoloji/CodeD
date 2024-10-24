namespace CodeD.Domain.Entities;

public class Category(int id,
                      string key,
                      string name,
                      List<Product>? products = null)
    : Entity<int>(id, key)
{
    public string Name { get; } = name;
    public List<Product> Products { get; } = products ?? [];
}
