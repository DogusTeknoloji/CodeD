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
}

public interface IEntityId
{
    Guid Value { get; }
}