using DronesWebApi.Core.Domain;
using DronesWebApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DronesWebApi.Persistence.Repositories
{
    public class DroneRepository: Repository<Drone>, IDroneRepository
    {
        public DroneRepository(DbContext context) : base(context)
        { }


    }
}
