using CodeD.Application.Queries;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

namespace CodeD.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddOpenApi();

        //builder.Services.AddMediatR(cfg =>
        //    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly)
        //        .RegisterServicesFromAssembly(typeof(PagableRequest).Assembly)
        //);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}