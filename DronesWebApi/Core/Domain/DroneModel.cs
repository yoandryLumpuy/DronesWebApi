using System.Collections.Generic;

namespace DronesWebApi.Core.Domain
{
    public class DroneModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public  int LightweightInGrams { get; set; }

        public int MiddleweightInGrams { get; set; }

        public int CruiserweightInGrams { get; set; }

        public int HeavyweightInGrams { get; set; }

        public virtual  ICollection<Drone> Drones { get; set; }
    }
}