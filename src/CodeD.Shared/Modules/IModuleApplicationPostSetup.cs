using Microsoft.Extensions.Configuration;

namespace CodeD.Domain.Abstractions.Modules;

public interface IModuleApplicationPostSetup
{
    Task PostSetup(IServiceProvider serviceProvider, IConfiguration configuration);
}