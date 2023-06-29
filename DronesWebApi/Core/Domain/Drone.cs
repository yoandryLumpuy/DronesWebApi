using System;
using System.Collections.Generic;
using DronesWebApi.Core.Domain.Enums;

namespace DronesWebApi.Core.Domain
{
    public class Drone
    {
        public int Id { get; set; }

        public string SerialNumber { get; set; }

        public int ModelId { get; set; }

        public DroneModel Model { get; set; }

        public int WeightLimitInGrams { get; set; }

        public int BatteryCapacityInPercentage { get; set; }

        public EDroneState State { get; set; }

        public virtual ICollection<Medication> LoadedMedications { get; set; }
    }
} 
