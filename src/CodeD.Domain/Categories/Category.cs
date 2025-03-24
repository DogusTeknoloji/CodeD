using CodeD.Domain.Abstractions;
using CodeD.Domain.Shared;

namespace CodeD.Domain.Categories;

public sealed class Category : Entity<CategoryId>
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1170:Use read-only auto-implemented property", Justification = "<Pending>")]
    public Title Title { get; private set; }

#pragma warning disable S1144 // Unused private types or members should be removed

    private Category() : base() // Required for EF
#pragma warning restore S1144 // Unused private types or members should be removed
    {
        Title = default!;
    }

    private Category(CategoryId id, Key key, Title title, WhoColumns whoColumns, ExternalReference externalReference)
        : base(id, key, whoColumns, externalReference)
    {
        Title = title;
    }

    public static Category Create(Key key, Title title, WhoColumns whoColumns, ExternalReference externalReference)
    {
        var c = new Category(CategoryId.New(), key, title, whoColumns, externalReference);
        // Domain Event
        c.AddDomainEvent(new CategoryCreatedEvent(c.Id));
        return c;
    }
}