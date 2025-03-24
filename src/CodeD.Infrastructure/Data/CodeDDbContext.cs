using CodeD.Domain.Abstractions;
using CodeD.Infrastructure.Data.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeD.Infrastructure.Data;

public class CodeDDbContext(
    DbContextOptions<CodeDDbContext> _options,
    IMediator _mediator,
    IDefaultTableSchema _defaultTableSchema)
    : Microsoft.EntityFrameworkCore.DbContext(_options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var schema = _defaultTableSchema.SchemaName;
        modelBuilder.HasDefaultSchema(schema);

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CategoryEntityMapping(schema));
        modelBuilder.ApplyConfiguration(new PostEntityMapping(schema));
    }
}