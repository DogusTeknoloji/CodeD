namespace CodeD.Api.Dto.Categories;

public record UpdateCategoryResponse(Guid Id, string Key, string Title, string? SourceProviderKey, string? SourceItemId, string? SourceVersion);