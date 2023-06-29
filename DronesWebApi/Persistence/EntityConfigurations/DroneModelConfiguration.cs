using DronesWebApi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DronesWebApi.Persistence.EntityConfigurations
{
    public class DroneModelConfiguration: IEntityTypeConfiguration<DroneModel>
    {
        public void Configure(EntityTypeBuilder<DroneModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        }
    }
}
