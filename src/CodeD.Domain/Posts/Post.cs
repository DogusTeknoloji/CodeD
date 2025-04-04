using CodeD.Domain.Abstractions;
using CodeD.Domain.Categories;
using CodeD.Domain.Shared;

namespace CodeD.Domain.Posts;

public sealed class Post : Entity<PostId>
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1170:Use read-only auto-implemented property", Justification = "<Pending>")]
    public CategoryId CategoryId { get; private set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1170:Use read-only auto-implemented property", Justification = "<Pending>")]
    public Title Title { get; private set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1170:Use read-only auto-implemented property", Justification = "<Pending>")]
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

    private Post(PostId id, Key key, CategoryId categoryId, Title title, Content content, WhoColumns whoColumns, ExternalReference? externalReference = null)
        : base(id, key, whoColumns, externalReference ?? ExternalReference.CreateInternal(id.Value.ToString(), whoColumns.ModifiedAt.ToString("O")))
    {
        CategoryId = categoryId;
        Title = title;
        Content = content;
    }

    public static Post Create(Key key, CategoryId categoryId, Title title, Content content, WhoColumns whoColumns, ExternalReference? externalReference = null)
    {
        var p = new Post(PostId.New(), key, categoryId, title, content, whoColumns, externalReference);
        // Domain Event
        p.AddDomainEvent(new PostCreatedEvent(p.Id));
        return p;
    }

    public void Publish()
    {
        if (PostStatus == PostStatus.Published)
            return;
        PostStatus = PostStatus.Published;
        PublishedAt = DateTimeOffset.UtcNow;
        // Domain Event
        AddDomainEvent(new PostPublishedEvent(Id));
    }
}