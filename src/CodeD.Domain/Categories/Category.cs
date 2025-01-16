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

public sealed class Category : Entity<CategoryId>
{
    public Name Name { get; private set; }

#pragma warning disable S1144 // Unused private types or members should be removed

    private Category() : base() // Required for EF
#pragma warning restore S1144 // Unused private types or members should be removed
    {
        Name = default!;
    }

    private Category(CategoryId id, Key key, Name name, WhoColumns whoColumns, ExternalReference externalReference)
        : base(id, key, whoColumns, externalReference)
    {
        Name = name;
    }

    public static Category Create(Key key, Name name, WhoColumns whoColumns, ExternalReference externalReference)
    {
        var c = new Category(CategoryId.New(), key, name, whoColumns, externalReference);
        // Domain Event
        c.AddDomainEvent(new CategoryCreatedEvent(c.Id));
        return c;
    }
}