using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeD.Domain.Abstractions;

public interface IModuleServicePreRegistration
{
    Task PreRegister(IServiceCollection serviceCollection, IConfiguration configuration);
}

public interface IModuleServicePostRegistration
{
    Task PostRegister(IServiceCollection serviceCollection, IConfiguration configuration);
}

public interface IModuleApplicationPreSetup
{
    Task PreSetup(IServiceProvider serviceProvider, IConfiguration configuration);
}

public interface IModuleApplicationPostSetup
{
    Task PostSetup(IServiceProvider serviceProvider, IConfiguration configuration);
}