using System.Collections.Generic;
using DronesWebApi.Core.Domain;

namespace DronesWebApi.Core.Repositories
{
    public interface IMedicationRepository : IRepository<Medication>
    {
        Medication GetWithDronesReferences(string code);

        IEnumerable<Medication> GetNotLoaded();
    }
}
