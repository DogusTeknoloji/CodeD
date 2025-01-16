using MediatR;

namespace CodeD.Application.Queries;

public record PagableRequest(int PageIndex, int PageSize);



public interface IQuery<TResponse> 
{

}

public interface IQueryHandler<in TRequest, TResponse> where TRequest : IQuery<TResponse>
{
    //
    // Summary:
    //     Handles a request
    //
    // Parameters:
    //   request:
    //     The request
    //
    //   cancellationToken:
    //     Cancellation token
    //
    // Returns:
    //     Response from the request
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}