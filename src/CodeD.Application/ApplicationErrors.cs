using CodeD.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeD.Application
{
    internal static class ApplicationErrors
    {
        public static Error CategoryNotFound(string key, string? additionalMessage = null)
            => new("application:category_not_found", $"Category [{key}] not found. {additionalMessage}");

        public static Error CategoryNotFound(Guid id, string? additionalMessage = null)
            => new("application:category_not_found", $"Category [#{id}] not found. {additionalMessage}");


        public static Error CategoryFound(string key, string? additionalMessage = null)
            => new("application:category_found", $"Category [{key}] found. {additionalMessage}");
    }
}