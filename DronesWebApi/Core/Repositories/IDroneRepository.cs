using System.Collections.Generic;
using DronesWebApi.Core.Domain;
using DronesWebApi.Persistence.Repositories;

namespace DronesWebApi.Core.Repositories
{
    public interface IDroneRepository : IRepository<Drone>
    {
        IPaginatedList<Drone> GetPaginatedWithLoadedMedications(int pageIndex, int pageSize);

        Drone GetWithLoadedMedications(int id);

        IEnumerable<Drone> GetAvailableForLoading();

        int TotalLoadInGrams(int id);
    }
}
