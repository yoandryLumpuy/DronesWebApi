using DronesWebApi.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DronesWebApi.Persistence.EntityConfigurations
{
    public class DroneConfiguration : IEntityTypeConfiguration<Drone>
    {
        public void Configure(EntityTypeBuilder<Drone> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(d => d.SerialNumber).IsRequired().HasMaxLength(100);

            builder.HasOne(d => d.Model)
                .WithMany(m => m.Drones)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
