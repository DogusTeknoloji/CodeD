using CodeD.Domain.Abstractions;

namespace CodeD.Domain.Posts;

public record PostPublishedEvent(PostId PostId) : IDomainEvent;