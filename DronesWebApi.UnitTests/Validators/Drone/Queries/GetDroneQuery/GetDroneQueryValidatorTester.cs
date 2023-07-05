using DronesWebApi.Core;
using FluentValidation;

namespace DronesWebApi.UnitTests.Validators.Drone.Queries.GetDroneQuery
{
    public class GetDroneQueryValidatorTester
    {
        public GetDroneQueryValidatorTester(IUnitOfWork unitOfWork)
        {
            //RuleFor(getDroneQuery => getDroneQuery.Id)
            //    .Must(id =>
            //    {
            //        var drone = unitOfWork.Drones.Get(id);

            //        return drone != null;

            //    }).WithMessage(getDroneQuery => $"Drone with id: '{getDroneQuery.Id}' was not found");
        }
    }
}
