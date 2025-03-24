using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CodeD.Infrastructure.Data;

public class CodeDDbContextDesignFactory : IDesignTimeDbContextFactory<CodeDDbContext>
{
    public CodeDDbContext CreateDbContext(string[] args)
    {
        // Build configuration
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Get connection string
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var schema = configuration.GetConnectionString("DefaultSchema");

        var optionsBuilder = new DbContextOptionsBuilder<CodeDDbContext>()
            .UseNpgsql(connectionString,
                    no => no.MigrationsHistoryTable("DbMigrationsHistory", configuration.GetConnectionString("DefaultSchema"))
                            .CommandTimeout(15));

        return new CodeDDbContext(optionsBuilder.Options, new NoMediator(), new NoDefaultTableSchema { SchemaName = schema });
    }

    private class NoMediator : IMediator
    {
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return AsyncEnumerable.Empty<TResponse>();
        }

        public IAsyncEnumerable<object> CreateStream(object request, CancellationToken cancellationToken = default)
        {
            return AsyncEnumerable.Empty<object>();
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<TResponse>(default);
        }

        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(default(object));
        }

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
        {
            return Task.CompletedTask;
        }
    }

    private class NoDefaultTableSchema : IDefaultTableSchema
    {
        public string SchemaName { get; set; }
    }
}