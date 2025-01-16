using CodeD.Domain.Abstractions;

namespace CodeD.Domain.Posts;

public sealed record PostId : IEntityId
{
    public static readonly PostId None = new();

    public Guid Value { get; private set; }

    private PostId()
    {
        Value = Guid.Empty;
    }

    public static PostId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentNullException(nameof(value));
        return new PostId { Value = value };
    }

    public static PostId New()
    {
        return new PostId { Value = Guid.NewGuid() };
    }
}
