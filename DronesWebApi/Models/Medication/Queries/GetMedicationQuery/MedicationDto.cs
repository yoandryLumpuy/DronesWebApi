using DronesWebApi.Commons.Mappings;
using System;
using DronesWebApi.Models.Drone.Queries.GetDroneQuery;

namespace DronesWebApi.Models.Medication.Queries.GetMedicationQuery
{
    public class MedicationDto: IMapFrom<Core.Domain.Medication>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int WeightInGrams { get; set; }

        public DateTime? DatetimeDelivery { get; set; }
    }
}
