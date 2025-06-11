#pragma warning disable RCS1170 // Use read-only auto-implemented property
namespace CodeD.Domain.Abstractions;

public abstract class Entity<TID>
    where TID : class, IEntityId
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected Entity()
    {
        Id = default!;
        Key = default!;
    }

    protected Entity(TID id, Key key, WhoColumns whoColumns, ExternalReference externalReference)
    {
        Id = id;
        Key = key;
        WhoColumns = whoColumns;
        ExternalReference = externalReference;
    }

    public TID Id { get; private set; }

    public Key Key { get; private set; }

    public long RowId { get; private set; } = 0;

    public WhoColumns WhoColumns { get; private set; } = WhoColumns.Empty();

    public ExternalReference ExternalReference { get; private set; } = ExternalReference.None;

    // soft delete
    public bool IsDeleted { get; private set; } = false;

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvent()
    {
        _domainEvents.Clear();
    }

    protected void SetWhoColumnsForUpdate()
    {
        // TODO: ModifiedBy kısmının Authentication'dan alınması lazım
        WhoColumns = WhoColumns.Create(WhoColumns.CreatedAt, WhoColumns.CreatedBy, DateTimeOffset.Now, Guid.Empty);
    }

    protected void SetExternalReference(ExternalReference externalReference)
    {
        ArgumentNullException.ThrowIfNull(externalReference);

        // external reference Change Cases

        // entity - null, request null => CreateInternal
        if (string.IsNullOrWhiteSpace(ExternalReference.ProviderKey) && string.IsNullOrWhiteSpace(externalReference.ProviderKey))
        {
            ExternalReference = ExternalReference.CreateInternal(Id.Value.ToString(), WhoColumns.ModifiedAt.ToString("O"));
            return;
        }
        // entity - null, request not null => Set 
        else if (string.IsNullOrWhiteSpace(ExternalReference.ProviderKey) && !string.IsNullOrWhiteSpace(externalReference.ProviderKey))
        {
            ExternalReference = externalReference;
            return;
        }

        // entity - not null, request null => do nothing
        else if (!string.IsNullOrWhiteSpace(ExternalReference.ProviderKey) && string.IsNullOrWhiteSpace(externalReference.ProviderKey))
        {
            return;
        }

        // entity - not null, request not null => Set
        ExternalReference = externalReference;
    }


    protected void BaseDelete()
    {
        IsDeleted = true;
    }
}
#pragma warning restore RCS1170 // Use read-only auto-implemented property
