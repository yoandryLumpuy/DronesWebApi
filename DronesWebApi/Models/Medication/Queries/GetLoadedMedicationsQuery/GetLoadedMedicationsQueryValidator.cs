using AutoMapper.Configuration;
using DronesWebApi.Core;
using DronesWebApi.Persistence;
using FluentValidation;

namespace DronesWebApi.Models.Medication.Queries.GetLoadedMedicationsQuery
{
    public class GetLoadedMedicationsQueryValidator: AbstractValidator<GetLoadedMedicationsQuery>
    {
        public GetLoadedMedicationsQueryValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(getLoadedMedicationsQuery => getLoadedMedicationsQuery.DroneId)
                .Must(droneId =>
                {
                    var drone = unitOfWork.Drones.Get(droneId);

                    return drone != null;

                }).WithMessage(getLoadedMedicationsQuery => $"Drone with id: '{getLoadedMedicationsQuery.DroneId}' was not found");
        }
    }
}
