using Data.Mappings.Base;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class ProfileMap : BaseMap<Profile>
    {
        public override void ConfigureOtherProperties(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profile");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        }
    }
}
