using System.Collections.Generic;
using System.Linq;
using DronesWebApi.Commons.Constants;
using DronesWebApi.Core.Domain;
using DronesWebApi.Core.Domain.Enums;
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
            DronesContext.Drones.Include(d => d.LoadedMedications).FirstOrDefault(d => d.Id == id);

        public IEnumerable<Drone> GetAvailableForLoading()
        {
            return DronesContext.Drones.Include(d => d.LoadedMedications)
                .Where(d => new List<EDroneState>() { EDroneState.Idle, EDroneState.Loading }.Contains(d.State)
                        && d.BatteryCapacityInPercentage >= Constants.MinPercentageOfBatteryLevelRequired).ToList();
        }

        public int TotalLoadInGrams(int id)
        {
            var drone = GetWithLoadedMedications(id) ?? new Drone(){ LoadedMedications = new List<Medication>()};

            return drone.LoadedMedications.Sum(m => m.WeightInGrams);
        }

        private DronesContext DronesContext => Context as DronesContext;
    }
}
