namespace CodeD.Domain.Categories;

public record Name
{
    private Name(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static Name Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Name is required");
        }
        if (value.Length > 100)
        {
            throw new ArgumentException("Name is too long");
        }
        return new Name(value);
    }
}