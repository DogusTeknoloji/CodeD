using CodeD.Domain.Abstractions.Modules;
using Scalar.AspNetCore;
using System.Reflection;

namespace CodeD.Api;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        List<string> scanAssemblyNameList = [];
        builder.Configuration.GetSection("ScanAssemblies").Bind(scanAssemblyNameList);
        var scanAssemblies = scanAssemblyNameList.Select(Assembly.Load).ToArray();

        var (preRegisterers, postRegisterers, preSetups, postSetups, mediatRServiceRegisterers) = GetRegistererAndSetups(scanAssemblies);

        foreach (var preRegisterer in preRegisterers)
        {
            await preRegisterer.PreRegister(builder.Services, builder.Configuration);
        }
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddOpenApi();

        builder.Services.AddMediatR(cfg =>
        {
            var currentAssembly = typeof(Program).Assembly;
            cfg.RegisterServicesFromAssembly(currentAssembly);

            foreach (var assembly in mediatRServiceRegisterers.SelectMany(x => x.GetAssemblies()).Where(x => x != currentAssembly))
            {
                cfg.RegisterServicesFromAssembly(assembly);
            }

            //cfg.RegisterServicesFromAssembly(typeof(Program).Assembly)
            //.RegisterServicesFromAssembly(typeof(PagableRequest).Assembly)
        });

        foreach (var postRegisterer in postRegisterers)
        {
            await postRegisterer.PostRegister(builder.Services, builder.Configuration);
        }

        var app = builder.Build();

        foreach (var preSetup in preSetups)
        {
            await preSetup.PreSetup(app.Services, app.Configuration);
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseAuthorization();

        app.MapControllers();

        foreach (var postSetup in postSetups)
        {
            await postSetup.PostSetup(app.Services, app.Configuration);
        }

        await app.RunAsync();
    }

    private static (List<IModuleServicePreRegistration> preRegisterers, List<IModuleServicePostRegistration> postRegisterers, List<IModuleApplicationPreSetup> preSetups, List<IModuleApplicationPostSetup> postSetups, List<IModuleMediatRServiceRegistration> mediatRServiceRegisterers) GetRegistererAndSetups(Assembly[] assemblies)
    {
        var preRegisterers = new List<IModuleServicePreRegistration>();
        var postRegisterers = new List<IModuleServicePostRegistration>();
        var preSetups = new List<IModuleApplicationPreSetup>();
        var postSetups = new List<IModuleApplicationPostSetup>();
        var mediatRServiceRegisterers = new List<IModuleMediatRServiceRegistration>();

        var types = assemblies.SelectMany(assembly => assembly.GetTypes()
                                                                   .Where(type => type.IsClass && !type.IsAbstract));

        foreach (var type in types)
        {
            object? instance = null; // InfrastructureRegisterer
            if (typeof(IModuleServicePreRegistration).IsAssignableFrom(type))
            {
                instance ??= Activator.CreateInstance(type);
                preRegisterers.Add((IModuleServicePreRegistration)instance!);
            }
            if (typeof(IModuleServicePostRegistration).IsAssignableFrom(type))
            {
                instance ??= Activator.CreateInstance(type);
                postRegisterers.Add((IModuleServicePostRegistration)instance!);
            }
            if (typeof(IModuleApplicationPreSetup).IsAssignableFrom(type))
            {
                instance ??= Activator.CreateInstance(type);
                preSetups.Add((IModuleApplicationPreSetup)instance!);
            }
            if (typeof(IModuleApplicationPostSetup).IsAssignableFrom(type))
            {
                instance ??= Activator.CreateInstance(type);
                postSetups.Add((IModuleApplicationPostSetup)instance!);
            }
            if (typeof(IModuleMediatRServiceRegistration).IsAssignableFrom(type))
            {
                instance ??= Activator.CreateInstance(type);
                mediatRServiceRegisterers.Add((IModuleMediatRServiceRegistration)instance!);
            }
        }
        return (preRegisterers, postRegisterers, preSetups, postSetups, mediatRServiceRegisterers);
    }
}