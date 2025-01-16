using CodeD.Domain.Abstractions;
using MediatR;

namespace CodeD.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;