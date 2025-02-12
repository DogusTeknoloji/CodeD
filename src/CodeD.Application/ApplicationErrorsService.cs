using CodeD.Application.Abstractions;
using CodeD.Domain.Abstractions;

namespace CodeD.Application
{
    internal class ApplicationErrorsService : IApplicationErrorsService
    {
        private readonly ISerializerProvider _serializerProvider;

        public ApplicationErrorsService(ISerializerProvider serializerProvider)
        {
            _serializerProvider = serializerProvider;
        }

        public Error CategoryNotFound(string key, object? additionalData = null)
        {
            var additionalMessage = additionalData != null
                ? _serializerProvider.Serialize(additionalData)
                : string.Empty;
            return new("application:category_not_found", $"Category [{key}] not found. {additionalMessage}");
        }
    }

}
