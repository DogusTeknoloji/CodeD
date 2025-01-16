using MediatR;

namespace CodeD.Application.Queries.Product;

//public class ProductListQuery : IProductListQuery
//{
//    public Task<IEnumerable<Product>> ListAsync()
//    {
//        throw new NotImplementedException();
//        //return await _productRepository.ListAsync();
//    }

//}

public record ProductListResponse(int Id, string Key, string Name, string? Description, double Price, int Stock, string? Image, int? CategoryId);

public record ProductListQuery(PagableRequest Page) : IRequest<PagableResponse<ProductListResponse>>;

public class ProductListQueryHandler : IRequestHandler<ProductListQuery, PagableResponse<ProductListResponse>>
{
    public Task<PagableResponse<ProductListResponse>> Handle(ProductListQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
