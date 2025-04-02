using CodeD.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeD.Infrastructure.Data.EntityConfigurations;

public abstract class EntityMapping<TEntity, TID>(string tableName, string? schemaName = null)
    : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity<TID>
    where TID : class, IEntityId
{
    protected static readonly ValueConverter<byte[], long> _converter = new(
      v => BitConverter.ToInt64(v, 0),
      v => BitConverter.GetBytes(v));

    protected virtual void ConfigureAdditional(EntityTypeBuilder<TEntity> builder)
    { }

    void IEntityTypeConfiguration<TEntity>.Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(tableName, schemaName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.RowId)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasIdentityOptions(startValue: 1)
            ;

        builder.HasIndex(x => x.RowId)
            .HasDatabaseName($"UQ_{tableName}_RowId")
            .IsUnique();

        builder.Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(64)
            .HasConversion(x => x.Value, x => Key.Create(x))
            ;

        builder.HasIndex(x => x.Key)
            .IsUnique();

        builder.OwnsOne(x => x.WhoColumns, wc =>
        {
            wc.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasColumnType("timestamp with time zone")
                ;

            wc.Property(x => x.CreatedBy)
                .IsRequired()
                .HasColumnName("CreatedBy");

            wc.Property(x => x.ModifiedAt)
                .IsRequired()
                .HasColumnName("ModifiedAt")
                .HasColumnType("timestamp with time zone");

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

        ConfigureAdditional(builder);
    }
}
