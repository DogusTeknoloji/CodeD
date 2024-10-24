namespace CodeD.Domain.Entities;

public class Product(int id,
                     string key,
                     string name,
                     string description,
                     double price,
                     int stock,
                     string? image)
    : Entity<int>(id, key)
{
    private string _name = name;
    private string? _description = description;
    private double _price = price;
    private int _stock = stock;
    private string? _image = image;
    private int? _categoryId;
    private Category? _category;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string? Description { get => _description; set => _description = value; }
    public double Price { get => _price; set => _price = value; }
    public int Stock { get => _stock; set => _stock = value; }
    public string? Image { get => _image; set => _image = value; }
    public int? CategoryId { get => _categoryId; set => _categoryId = value; }
    public Category? Category { get => _category; }

    public void SetCategory(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        _category = category;
    }
}
