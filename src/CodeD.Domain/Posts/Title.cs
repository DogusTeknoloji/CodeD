namespace CodeD.Domain.Posts;

public sealed record Title
{
    public static readonly Title None = new();

    public string Value { get; private set; }
    private Title()
    {
        Value = string.Empty;
    }
    private Title(string value)
    {
        Value = value;
    }
    public static Title Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));
        return new Title(value);
    }

}
