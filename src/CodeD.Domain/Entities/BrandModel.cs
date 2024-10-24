namespace CodeD.Domain.Entities;

public class BrandModel : Entity<int>
{
    public string Name { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    public List<Product> Products { get; set; }
}