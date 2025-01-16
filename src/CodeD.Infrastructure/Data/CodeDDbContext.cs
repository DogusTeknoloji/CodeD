using CodeD.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CodeD.Infrastructure.Data;

public class CodeDDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public CodeDDbContext(DbContextOptions<CodeDDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryEntityMapping).Assembly);
    }
}