using CodeD.Domain.Abstractions;
using CodeD.Domain.Abstractions.Modules;
using CodeD.Domain.Categories;
using CodeD.Domain.Posts;
using CodeD.Domain.Shared;
using CodeD.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CodeD.Infrastructure.Data
{
    public class InfrastructureRegisterer : IModuleServicePreRegistration, IModuleServicePostRegistration, IModuleMediatRServiceRegistration
    {
        public Task PreRegister(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton<IDefaultTableSchema, DefaultTableSchema>();

            serviceCollection.AddDbContext<CodeDDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    no => no.MigrationsHistoryTable("DbMigrationsHistory", configuration.GetConnectionString("DefaultSchema"))
                            .CommandTimeout(15));
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
                options.EnableServiceProviderCaching();

                options
                .UseSeeding((c, _) =>
                {
                    var willSaveChange = false;
                    var defaultCategory = c.Set<Category>().FirstOrDefault(x => x.Key.Value == "Default");
                    if (defaultCategory == null)
                    {
                        c.Add(Category.Create(Key.Create("Default"), Title.Create("Default Category"), WhoColumns.Create(DateTimeOffset.Now, Guid.Empty, DateTimeOffset.Now, Guid.Empty), ExternalReference.Create("CodeD", "Default", null)));
                        willSaveChange = true;
                    }

                    if (willSaveChange)
                    {
                        c.SaveChanges();
                    }
                })
                .UseAsyncSeeding(async (c, _, ct) =>
                {
                    var willSaveChange = false;
                    var defaultCategory = await c.Set<Category>().FirstOrDefaultAsync(x => x.Key.Value == "Default", ct);
                    if (defaultCategory == null)
                    {
                        c.Add(Category.Create(Key.Create("Default"), Title.Create("Default Category"), WhoColumns.Create(DateTimeOffset.Now, Guid.Empty, DateTimeOffset.Now, Guid.Empty), ExternalReference.Create("CodeD", "Default", null)));
                        willSaveChange = true;
                    }

                    if (willSaveChange)
                    {
                        await c.SaveChangesAsync(ct);
                    }
                });
            });

            serviceCollection.AddScoped<IUnitOfWork>(c => c.GetRequiredService<CodeDDbContext>());
            serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
            serviceCollection.AddScoped<IPostRepository, PostRepository>();
            return Task.CompletedTask;
        }

        public Task PostRegister(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return Task.CompletedTask;
        }

        public Assembly[] GetAssemblies() => [typeof(InfrastructureRegisterer).Assembly];
    }
}