using CodeD.Domain.Abstractions;

namespace CodeD.Domain.Categories;

public record CategoryCreatedEvent(CategoryId Id) : IDomainEvent;