using CodeD.Domain.Abstractions;

namespace CodeD.Domain.Posts;

public record PostCreatedEvent(PostId PostId) : IDomainEvent;