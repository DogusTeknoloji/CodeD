using CodeD.Domain.Abstractions.Modules;
using System.Reflection;

namespace CodeD.Application;

public class ApplicationRegisterer : IModuleMediatRServiceRegistration
{
    public Assembly[] GetAssemblies() => [typeof(ApplicationRegisterer).Assembly];
}