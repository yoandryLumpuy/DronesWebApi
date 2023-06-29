using DronesWebApi.Core.Domain;
using DronesWebApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DronesWebApi.Persistence.Repositories
{
    public class DroneModelRepository: Repository<DroneModel>, IDroneModelRepository
    {
        public DroneModelRepository(DbContext context) : base(context)
        { }


    }
}
