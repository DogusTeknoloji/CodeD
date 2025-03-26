using CodeD.Application.Abstractions.Messaging;

namespace CodeD.Application.Commands.Categories;

public sealed class CreateCategoryCommand : ICommand<Guid>
{
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    public string SourceProviderKey { get; set; } = string.Empty;

    public string SourceItemId { get; set; } = string.Empty;

    public string? SourceVersion { get; set; } = null;
}
