namespace CodeD.Domain.Entities;

public class Brand : Entity<int>
{
    public string Name { get; set; }
    public List<BrandModel> BrandModels { get; set; }
    public List<Product> Products { get; set; }
}
