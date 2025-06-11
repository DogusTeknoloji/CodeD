using CodeD.Application.Abstractions.Messaging;

namespace CodeD.Application.Commands.Categories;

public sealed class DeleteCategoryCommand : ICommand
{
    public Guid? Id { get; set; }

    public string? Key { get; set; }
}