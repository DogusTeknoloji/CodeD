namespace CodeD.Domain.Abstractions;

public interface IEntityId
{
    Guid Value { get; }
}