using Microsoft.Extensions.Configuration;

namespace CodeD.Domain.Abstractions.Modules;

public interface IModuleApplicationPreSetup
{
    Task PreSetup(IServiceProvider serviceProvider, IConfiguration configuration);
}
