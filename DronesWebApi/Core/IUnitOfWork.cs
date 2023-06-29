using DronesWebApi.Core.Repositories;
using System;

namespace DronesWebApi.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IDroneRepository Drones { get; }

        IDroneModelRepository DroneModels { get; }

        IMedicationRepository Medications { get; }

        int Complete();
    }
}