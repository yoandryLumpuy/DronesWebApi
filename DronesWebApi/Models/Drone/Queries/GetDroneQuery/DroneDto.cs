using AutoMapper;
using DronesWebApi.Commons.Mappings;
using DronesWebApi.Core.Domain.Enums;
using DronesWebApi.Models.DroneModel.Queries.GetDroneModelQuery;
using System.Collections.Generic;
using DronesWebApi.Models.Medication.Queries.GetMedicationQuery;

namespace DronesWebApi.Models.Drone.Queries.GetDroneQuery
{
    public class DroneDto : IMapFrom<Core.Domain.Drone>
    {
        public int Id { get; set; }

        public string SerialNumber { get; set; }

        public int ModelId { get; set; }

        public DroneModelDto Model { get; set; }

        public int WeightLimitInGrams { get; set; }

        public int BatteryCapacityInPercentage { get; set; }

        public EDroneState State { get; set; }


        public virtual ICollection<MedicationDto> LoadedMedications { get; set; }

        public virtual ICollection<MedicationDto> DeliveredMedications { get; set; }
    }
}
