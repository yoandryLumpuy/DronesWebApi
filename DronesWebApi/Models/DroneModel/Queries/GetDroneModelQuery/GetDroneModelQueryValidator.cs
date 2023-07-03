using DronesWebApi.Core;
using FluentValidation;

namespace DronesWebApi.Models.DroneModel.Queries.GetDroneModelQuery
{
    public class GetDroneModelQueryValidator: AbstractValidator<GetDroneModelQuery>
    {
        public GetDroneModelQueryValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(getDroneModelQuery => getDroneModelQuery.ModelId)
                .Must(modelId =>
                {
                    var droneModel = unitOfWork.DroneModels.Get(modelId);

                    return droneModel != null;

                }).WithMessage(getDroneModelQuery => $"Drone model with id: '{getDroneModelQuery.ModelId}' was not found");
        }
    }
}
