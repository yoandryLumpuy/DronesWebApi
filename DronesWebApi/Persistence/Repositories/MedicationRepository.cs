using System.Collections.Generic;
using System.Linq;
using DronesWebApi.Core.Domain;
using DronesWebApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DronesWebApi.Persistence.Repositories
{
    public class MedicationRepository: Repository<Medication>, IMedicationRepository
    {
        public MedicationRepository(DbContext context) : base(context)
        { }

        private DronesContext DronesContext => Context as DronesContext;

        public Medication GetWithDronesReferences(string code)
        {
            return DronesContext.Medications.Include(m => m.Drone)
                .Include(m => m.DeliveredByDrone)
                .FirstOrDefault(m => m.Code == code);
        }

        public IEnumerable<Medication> GetNotLoaded()
        {
            return DronesContext.Medications
                    .Where(m => m.Drone == null && !m.DatetimeDelivery.HasValue && m.DeliveredByDrone == null)
                    .ToList();
        }
    }
}
