namespace CodeD.Domain.Abstractions;

public record Key
{
    private const string NONEKEY = "<13^-_NONE_-^31>";

    public static readonly Key None = new() { Value = NONEKEY };

    public string Value { get; private set; }

    private Key()
    {
        Value = string.Empty;
    }

    public static Key Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        if (value == NONEKEY) throw new KeyValueFormatException("Key value cannot be NONEKEY");

        return new Key { Value = value };
    }
}