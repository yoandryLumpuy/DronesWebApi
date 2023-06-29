using DronesWebApi.Core.Domain;
using DronesWebApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DronesWebApi.Persistence.Repositories
{
    public class MedicationRepository: Repository<Medication>, IMedicationRepository
    {
        public MedicationRepository(DbContext context) : base(context)
        { }


    }
}
