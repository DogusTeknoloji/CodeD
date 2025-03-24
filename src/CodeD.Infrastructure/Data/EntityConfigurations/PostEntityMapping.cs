using CodeD.Domain.Categories;
using CodeD.Domain.Posts;
using CodeD.Domain.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeD.Infrastructure.Data.EntityConfigurations;

public class PostEntityMapping(string? schema = null)
    : EntityMapping<Post, PostId>("Post", schema)
{
    protected override void ConfigureAdditional(EntityTypeBuilder<Post> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(x => x.Value, x => PostId.Create(x))
            ;

        builder.Property(x => x.CategoryId)
            .IsRequired()
            .HasConversion(x => x.Value, x => CategoryId.Create(x))
            ;

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .IsRequired()
            ;

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(64)
            .HasConversion(x => x.Value, x => Title.Create(x))
            ;

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(int.MaxValue)
            .HasConversion(x => x.Value, x => Content.Create(x))
            ;

        builder.Property(x => x.PostStatus)
            .IsRequired()
            ;
        builder.Property(x => x.PublishedAt)
            .IsRequired(false)
            ;
    }
}