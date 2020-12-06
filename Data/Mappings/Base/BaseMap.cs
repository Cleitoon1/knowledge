using System;
using Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.Base
{
    public abstract class BaseMap<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.ModifiedDate).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.ModifiedBy);
            ConfigureOtherProperties(builder);
        }

        public abstract void ConfigureOtherProperties(EntityTypeBuilder<TEntity> builder);
    }
}
