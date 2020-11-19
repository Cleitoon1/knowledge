using Data.Mappings.Base;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class UserMap : BaseMap<User>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Mail).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(200);
            builder.Property(x => x.MobilePhone).HasColumnType("char(15)");
            builder.Property(x => x.BirthDate);
            builder.HasOne(x => x.Profile).WithMany(x => x.Users).HasForeignKey(x => x.ProfileId).IsRequired();
        }
    }
}
