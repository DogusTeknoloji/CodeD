namespace CodeD.Domain.Abstractions;

public record ExternalReference
{
    public static readonly ExternalReference None = new();

    public string ProviderKey { get; private set; }

    public string ItemId { get; private set; }

    public string? Version { get; private set; }

    private ExternalReference()
    {
        ProviderKey = string.Empty;
        ItemId = string.Empty;
        Version = null;
    }

    public static ExternalReference Create(string providerKey, string itemId, string? version)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(providerKey);
        ArgumentException.ThrowIfNullOrWhiteSpace(itemId);

        return new ExternalReference()
        {
            ProviderKey = providerKey,
            ItemId = itemId,
            Version = version
        };
    }
}