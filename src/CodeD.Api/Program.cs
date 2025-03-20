using CodeD.Domain.Abstractions;
using Scalar.AspNetCore;
using System.Reflection;

namespace CodeD.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        List<string> scanAssemblyNameList = [];
        builder.Configuration.GetSection("ScanAssemblies").Bind(scanAssemblyNameList);
        var scanAssemblies = scanAssemblyNameList.Select(Assembly.Load).ToArray();

        var (preRegisterers, postRegisterers, preSetups, postSetups) = GetRegistererAndSetups(scanAssemblies);

        foreach (var preRegisterer in preRegisterers)
        {
            preRegisterer.PreRegister(builder.Services, builder.Configuration);
        }
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddOpenApi();

        //builder.Services.AddMediatR(cfg =>
        //    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly)
        //        .RegisterServicesFromAssembly(typeof(PagableRequest).Assembly)
        //)

        foreach (var postRegisterer in postRegisterers)
        {
            postRegisterer.PostRegister(builder.Services, builder.Configuration);
        }

        var app = builder.Build();

        foreach (var preSetup in preSetups)
        {
            preSetup.PreSetup(app.Services, app.Configuration);
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
            postSetup.PostSetup(app.Services, app.Configuration);
        }

        app.Run();
    }

    private static (List<IModuleServicePreRegistration> preRegisterers, List<IModuleServicePostRegistration> postRegisterers, List<IModuleApplicationPreSetup> preSetups, List<IModuleApplicationPostSetup> postSetups) GetRegistererAndSetups(Assembly[] assemblies)
    {
        var preRegisterers = new List<IModuleServicePreRegistration>();
        var postRegisterers = new List<IModuleServicePostRegistration>();
        var preSetups = new List<IModuleApplicationPreSetup>();
        var postSetups = new List<IModuleApplicationPostSetup>();

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
        }
        return (preRegisterers, postRegisterers, preSetups, postSetups);
    }
}