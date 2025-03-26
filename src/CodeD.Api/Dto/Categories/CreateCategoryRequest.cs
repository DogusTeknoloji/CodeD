namespace CodeD.Api.Dto.Categories;

public record CreateCategoryRequest(string Key, string Title, string? SourceProviderKey, string? SourceItemId, string? SourceVersion);