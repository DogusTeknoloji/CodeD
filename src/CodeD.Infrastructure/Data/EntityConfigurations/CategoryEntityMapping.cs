using CodeD.Domain.Categories;
using CodeD.Domain.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeD.Infrastructure.Data.EntityConfigurations;

public class CategoryEntityMapping(string? schema = null)
    : EntityMapping<Category, CategoryId>("Category", schema)
{
    protected override void ConfigureAdditional(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(x => x.Value, x => CategoryId.Create(x))
            ;

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(64)
            .HasConversion(x => x.Value, x => Title.Create(x))
            ;
    }
}