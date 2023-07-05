using DronesWebApi.Core;
using DronesWebApi.Models.Drone.Queries.GetDroneBatteryLevelQuery;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.Drone.Queries.GetDroneBatteryLevelQuery
{
    public class GetDroneBatteryLevelQueryValidatorTester
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;

        private readonly GetDroneBatteryLevelQueryValidator _validator;

        public GetDroneBatteryLevelQueryValidatorTester()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _validator = new GetDroneBatteryLevelQueryValidator(_unitOfWork.Object);
        }

        [Fact]
        public void Should_not_throw_error_when_drone_exists_in_database()
        {
            var model = new Models.Drone.Queries.GetDroneBatteryLevelQuery.GetDroneBatteryLevelQuery()
            {
                DroneId = 1
            };

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Drone());

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.DroneId);
        }

        [Fact]
        public void Should_throw_error_when_drone_does_not_exists_in_database()
        {
            var model = new Models.Drone.Queries.GetDroneBatteryLevelQuery.GetDroneBatteryLevelQuery()
            {
                DroneId = 1
            };

            _unitOfWork.Setup(x => x.Drones.Get(It.IsAny<object[]>())).Returns((Core.Domain.Drone)null);

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.DroneId);
        }
    }
}
