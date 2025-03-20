using CodeD.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeD.Infrastructure.Data
{
    public class InfrastructureRegisterer : IModuleServicePreRegistration, IModuleServicePostRegistration
    {
        public Task PreRegister(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<CodeDDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            serviceCollection.AddScoped<IUnitOfWork>(c => c.GetService<CodeDDbContext>()!);
            return Task.CompletedTask;
        }

        public Task PostRegister(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return Task.CompletedTask;
        }
    }
}