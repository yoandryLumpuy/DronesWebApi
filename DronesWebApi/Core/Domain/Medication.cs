using System;

namespace DronesWebApi.Core.Domain
{
    public class Medication
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public  int WeightInGrams { get; set; }

        public Drone  Drone { get; set; }


        public DateTime? DatetimeDelivery { get; set; }

        public Drone DeliveredByDrone { get; set; }
    }
}
