namespace CodeD.Api.Dto.Products
{
    public record CreateProductResponse(int Id, string Key, string Name, string? Description, double Price, int Stock, string? Image, int? CategoryId);
}