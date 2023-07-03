using DronesWebApi.Core;
using FluentValidation;

namespace DronesWebApi.Models.Drone.Queries.GetDroneQuery
{
    public class GetDroneQueryValidator: AbstractValidator<GetDroneQuery>
    {
        public GetDroneQueryValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(getDroneQuery => getDroneQuery.Id)
                .Must(id =>
                {
                    var drone = unitOfWork.Drones.Get(id);

                    return drone != null;

                }).WithMessage(getDroneQuery => $"Drone with id: '{getDroneQuery.Id}' was not found");
        }
    }
}
