using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeD.Domain.Abstractions.Modules;

public interface IModuleServicePostRegistration
{
    Task PostRegister(IServiceCollection serviceCollection, IConfiguration configuration);
}
