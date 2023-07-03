using System;

namespace DronesWebApi.Models.Drone.Queries.GetDroneBatteryLevelQuery
{
    public class GetDroneBatteryLevelQueryResponse
    {
        public int DroneId { get; set; }

        public int BatteryLevel { get; set; }
    }
}
