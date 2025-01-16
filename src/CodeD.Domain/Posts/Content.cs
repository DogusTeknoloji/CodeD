namespace CodeD.Domain.Posts;

public sealed record Content
{
    public static readonly Content None = new();
    public string Value { get; private set; }
    private Content()
    {
        Value = string.Empty;
    }
    private Content(string value)
    {
        Value = value;
    }
    public static Content Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));
        return new Content(value);
    }
}
