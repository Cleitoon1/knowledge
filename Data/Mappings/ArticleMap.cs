using Data.Mappings.Base;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class ArticleMap : BaseMap<Article>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Article");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.ImageUrl).HasMaxLength(300);
            builder.Property(x => x.Content).IsRequired();
            builder.HasOne(x => x.Category).WithMany(x => x.Articles).HasForeignKey(x => x.CategoryId).IsRequired();
        }
    }
}
