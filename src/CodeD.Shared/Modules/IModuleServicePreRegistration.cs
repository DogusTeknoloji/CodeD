using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CodeD.Domain.Abstractions.Modules;

public interface IModuleServicePreRegistration
{
    Task PreRegister(IServiceCollection serviceCollection, IConfiguration configuration);
}
public interface IModuleMediatRServiceRegistration
{
    Assembly[] GetAssemblies();
}
