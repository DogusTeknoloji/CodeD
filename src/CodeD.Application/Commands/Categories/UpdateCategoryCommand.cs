using CodeD.Application.Abstractions.Messaging;
using CodeD.Domain.Categories;

namespace CodeD.Application.Commands.Categories;

public sealed class UpdateCategoryCommand : ICommand<Category?>
{
    public Guid? Id { get; set; }

    public string? Key { get; set; }

    public string Title { get; set; } = string.Empty;

    public string SourceProviderKey { get; set; } = string.Empty;

    public string SourceItemId { get; set; } = string.Empty;

    public string? SourceVersion { get; set; } = null;
}
