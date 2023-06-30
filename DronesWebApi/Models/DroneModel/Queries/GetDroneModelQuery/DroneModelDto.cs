using AutoMapper;
using DronesWebApi.Commons.Mappings;
using DronesWebApi.Core.Domain.Enums;

namespace DronesWebApi.Models.DroneModel.Queries.GetDroneModelQuery
{
    public class DroneModelDto: IMapFrom<Core.Domain.DroneModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LightweightInGrams { get; set; }

        public int MiddleweightInGrams { get; set; }

        public int CruiserweightInGrams { get; set; }

        public int HeavyweightInGrams { get; set; }
    }
}
