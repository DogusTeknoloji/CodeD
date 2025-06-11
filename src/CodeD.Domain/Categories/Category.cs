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

    private Category(CategoryId id, Key key, Title title, WhoColumns whoColumns, ExternalReference? externalReference = null)
        : base(id, key, whoColumns, externalReference ?? ExternalReference.CreateInternal(id.Value.ToString(), whoColumns.ModifiedAt.ToString("O")))
    {
        Title = title;
    }

    public static Category Create(Key key, Title title, WhoColumns whoColumns, ExternalReference? externalReference = null)
    {
        var c = new Category(CategoryId.New(), key, title, whoColumns, externalReference);
        // Domain Event
        c.AddDomainEvent(new CategoryCreatedEvent(c.Id));
        return c;
    }

    public void Update(Title title, ExternalReference? externalReference = null)
    {
        SetWhoColumnsForUpdate();
        Title = title;
        SetExternalReference(externalReference ?? ExternalReference.CreateInternal(Id.Value.ToString(), WhoColumns.ModifiedAt.ToString("O")));
        // Domain Event
        AddDomainEvent(new CategoryUpdatedEvent(Id));
    }

    public void Delete()
    {
        BaseDelete();

        // Domain Event
        AddDomainEvent(new CategoryDeletedEvent(Id));
    }

}