namespace CodeD.Api.Dto.Categories;

public record CreateCategoryResponse(Guid Id, string Key, string Title, string? SourceProviderKey, string? SourceItemId, string? SourceVersion);
