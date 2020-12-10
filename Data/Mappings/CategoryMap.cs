using Data.Mappings.Base;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class CategoryMap : BaseMap<Category>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.HasOne(x => x.Parent).WithMany(x => x.Childrens).HasForeignKey(x => x.ParentId);
        }
    }
}
