using CodeD.Domain.Abstractions;
using CodeD.Domain.Categories;
using CodeD.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;

//using

namespace CodeD.Infrastructure.Data.EntityConfigurations;

public class CategoryEntityMapping : IEntityTypeConfiguration<Category>
{
    private static readonly ValueConverter<byte[], long> _converter = new ValueConverter<byte[], long>(
      v => BitConverter.ToInt64(v, 0),
      v => BitConverter.GetBytes(v));

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(x => x.Value, x => CategoryId.Create(x))
            ;

        builder.Property(x => x.RowId)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasIdentityOptions(startValue: 1)
            ;

        builder.HasIndex(x => x.RowId)
            .HasDatabaseName("UQ_Category_RowId")
            .IsUnique()
            .HasAnnotation("Npgsql:IndexMethod", "btree")
            .HasAnnotation("Npgsql:IndexClustered", true);

        builder.Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(64)
            .HasConversion(x => x.Value, x => Key.Create(x))
            ;

        builder.HasIndex(x => x.Key)
            .IsUnique();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(64)
            .HasConversion(x => x.Value, x => Title.Create(x))
            ;

        builder.OwnsOne(x => x.WhoColumns, wc =>
        {
            wc.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                ;

            wc.Property(x => x.CreatedBy)
                .IsRequired()
                .HasColumnName("CreatedBy");

            wc.Property(x => x.ModifiedAt)
                .IsRequired()
                .HasColumnName("ModifiedAt");

            wc.Property(x => x.ModifiedBy)
                .IsRequired()
                .HasColumnName("ModifiedBy");
        });

        builder.OwnsOne(m => m.ExternalReference, er =>
        {
            er.Property(x => x.ProviderKey)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("SourceProviderKey")
                ;
            er.Property(x => x.ItemId)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("SourceItemId")
                ;
            er.Property(x => x.Version)
                .HasMaxLength(64)
                .HasColumnName("SourceVersion")
                ;
        });

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("IsDeleted")
            ;

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder
              .Property<byte[]>("Timestamp")
              .HasColumnName("xmin")
              .HasColumnType("xid")
              .IsConcurrencyToken()
              .ValueGeneratedOnAddOrUpdate()
              .HasConversion(_converter);
    }
}