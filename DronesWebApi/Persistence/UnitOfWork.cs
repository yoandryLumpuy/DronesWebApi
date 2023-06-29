using DronesWebApi.Core;
using DronesWebApi.Core.Repositories;
using DronesWebApi.Persistence.Repositories;

namespace DronesWebApi.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DronesContext _context;

        public IDroneRepository Drones { get; }

        public IDroneModelRepository DroneModels { get; }

        public IMedicationRepository Medications { get; }

        public UnitOfWork(DronesContext context)
        {
            _context = context;
            Drones = new DroneRepository(context);
            DroneModels = new DroneModelRepository(context);
            Medications = new MedicationRepository(context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}