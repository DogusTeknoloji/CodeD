using CodeD.Domain.Abstractions;
using CodeD.Domain.Categories;

namespace CodeD.Domain.Posts;

public sealed class Post : Entity<PostId>
{
    public CategoryId CategoryId { get; private set; }
    public Title Title { get; private set; }
    public Content Content { get; private set; }
    public PostStatus PostStatus { get; private set; } = PostStatus.Draft;
    public DateTimeOffset? PublishedAt { get; private set; } = null;

#pragma warning disable S1144 // Unused private types or members should be removed

    private Post() // Required for EF
#pragma warning restore S1144 // Unused private types or members should be removed
    {
        CategoryId = CategoryId.None;
        Title = Title.None;
        Content = Content.None;
    }

    private Post(PostId id, Key key, CategoryId categoryId, Title title, Content content, WhoColumns whoColumns, ExternalReference externalReference)
        : base(id, key, whoColumns, externalReference)
    {
        CategoryId = categoryId;
        Title = title;
        Content = content;
    }

    public static Post Create(Key key, CategoryId categoryId, Title title, Content content, WhoColumns whoColumns, ExternalReference externalReference)
    {
        var p = new Post(PostId.New(), key, categoryId, title, content, whoColumns, externalReference);
        // Domain Event
        p.AddDomainEvent(new PostCreatedEvent(p.Id));
        return p;
    }
}