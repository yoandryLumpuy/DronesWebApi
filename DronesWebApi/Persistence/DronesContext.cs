using DronesWebApi.Core.Domain;
using DronesWebApi.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DronesWebApi.Persistence
{
    public class DronesContext : DbContext
    {
        public DbSet<Drone> Drones { get; set; }

        public DbSet<DroneModel> DroneModels { get; set; }

        public DbSet<Medication> Medications { get; set; }

        public DbSet<Image> Images { get; set; }

        public DronesContext(DbContextOptions<DronesContext> options): base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DroneConfiguration());
            modelBuilder.ApplyConfiguration(new DroneModelConfiguration());
            modelBuilder.ApplyConfiguration(new MedicationConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
        }
    }
}
