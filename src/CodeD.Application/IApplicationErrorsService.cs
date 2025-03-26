using CodeD.Domain.Abstractions;

namespace CodeD.Application;

internal interface IApplicationErrorsService
{
    Error CategoryNotFound(string key, object? additionalData = null);
}
