using System.Linq;
using DronesWebApi.Core.Domain;
using DronesWebApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DronesWebApi.Persistence.Repositories
{
    public class DroneRepository: Repository<Drone>, IDroneRepository
    {
        public DroneRepository(DronesContext context) : base(context)
        { }

        public IPaginatedList<Drone> GetPaginatedWithLoadedMedications(int pageIndex, int pageSize)
        {
            return PaginatedList<Drone>.CreateAsync(
                source: DronesContext.Drones.Include(d => d.LoadedMedications).AsQueryable(), 
                pageIndex, pageSize).Result;
        }

        public Drone GetWithLoadedMedications(int id) => 
            DronesContext.Drones.Include(d => d.LoadedMedications).SingleOrDefault(d => d.Id == id);

        private DronesContext DronesContext => Context as DronesContext;
    }
}
