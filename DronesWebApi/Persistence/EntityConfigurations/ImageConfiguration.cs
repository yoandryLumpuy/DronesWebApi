using DronesWebApi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DronesWebApi.Persistence.EntityConfigurations
{
    public class ImageConfiguration: IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.FileName).IsRequired().HasMaxLength(100);

            builder.Property(i => i.ContentType).IsRequired().HasMaxLength(100);

            builder.Property(i => i.Data).IsRequired();
        }
    }
}
