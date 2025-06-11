using CodeD.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeD.Application
{
    public static class ApplicationErrors
    {
        public const string CategoryNotFoundCode = "application:category_not_found";
        public const string CategoryFoundCode = "application:category_found";

        internal static Error CategoryNotFound(string key, string? additionalMessage = null)
            => new(CategoryNotFoundCode, $"Category [{key}] not found. {additionalMessage}");

        internal static Error CategoryNotFound(Guid id, string? additionalMessage = null)
            => new(CategoryNotFoundCode, $"Category [#{id}] not found. {additionalMessage}");

        internal static Error CategoryFound(string key, string? additionalMessage = null)
            => new(CategoryFoundCode, $"Category [{key}] found. {additionalMessage}");
    }
}