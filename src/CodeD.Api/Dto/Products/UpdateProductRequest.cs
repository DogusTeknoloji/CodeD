namespace CodeD.Api.Dto.Products
{
    public record UpdateProductRequest(string Key, string Name, string? Description, double Price, int Stock, string? Image, int? CategoryId);
}