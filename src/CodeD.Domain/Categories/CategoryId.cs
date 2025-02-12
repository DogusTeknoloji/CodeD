using CodeD.Domain.Abstractions;

namespace CodeD.Domain.Categories;

public sealed record CategoryId : IEntityId
{
    public static readonly CategoryId None = new();

    public Guid Value { get; private set; }

    private CategoryId()
    {
        Value = Guid.Empty;
    }

    public static CategoryId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentNullException(nameof(value));
        return new CategoryId { Value = value };
    }

    public static CategoryId New()
    {
        return new CategoryId { Value = Guid.NewGuid() };
    }
}
