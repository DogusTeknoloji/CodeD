using CodeD.Domain.Abstractions;

namespace CodeD.Domain.Categories;

public record CategoryCreatedEvent(CategoryId Id) : IDomainEvent;

public record CategoryUpdatedEvent(CategoryId Id) : IDomainEvent;

public record CategoryDeletedEvent(CategoryId Id) : IDomainEvent;