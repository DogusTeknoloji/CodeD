namespace CodeD.Domain.Abstractions;

public record WhoColumns
{
    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset ModifiedAt { get; private set; }
    public Guid ModifiedBy { get; private set; }

    private WhoColumns() { }

    public static WhoColumns Create(DateTimeOffset createdAt, Guid createdBy, DateTimeOffset modifiedAt, Guid modifiedBy)
        => new()
        {
            CreatedAt = createdAt,
            CreatedBy = createdBy,
            ModifiedAt = modifiedAt,
            ModifiedBy = modifiedBy
        };

    public static WhoColumns Empty() =>
        new()
        {
            CreatedAt = DateTimeOffset.Now,
            CreatedBy = Guid.Empty,
            ModifiedAt = DateTimeOffset.Now,
            ModifiedBy = Guid.Empty
        };

}