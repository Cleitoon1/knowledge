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
            builder.Property(x => x.CreatedBy).ValueGeneratedOnAdd();
            builder.Property(x => x.ModifiedDate).ValueGeneratedOnUpdate();
            ConfigureOtherProperties(builder);
        }

        public abstract void ConfigureOtherProperties(EntityTypeBuilder<TEntity> builder);
    }
}
