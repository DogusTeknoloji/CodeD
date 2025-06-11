using MediatR;

namespace CodeD.Application.Queries.Categories;

public record CategoryListResponse(int Id, string Key, string Title);

public record CategoryListQuery(PagableRequest Page) : IRequest<PagableResponse<CategoryListResponse>>;

public class CategoryListQueryHandler : IRequestHandler<CategoryListQuery, PagableResponse<CategoryListResponse>>
{
    public Task<PagableResponse<CategoryListResponse>> Handle(CategoryListQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}