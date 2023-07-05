using DronesWebApi.Core;
using DronesWebApi.Models.Medication.Queries.GetLoadedMedicationsQuery;
using DronesWebApi.Models.Medication.Queries.GetMedicationQuery;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.Medication.Queries.GetMedicationQuery
{
    public class GetMedicationQueryValidatorTester
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly GetMedicationQueryValidator _validator;

        public GetMedicationQueryValidatorTester()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _validator = new GetMedicationQueryValidator(_unitOfWork.Object);
        }

        [Fact]
        public void Should_throw_error_when_medication_does_not_exists()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns((Core.Domain.Medication)null);

            var model = new Models.Medication.Queries.GetMedicationQuery.GetMedicationQuery()
            {
                Code = "AA"
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Fact]
        public void Should_not_throw_error_when_medication_exists()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            var model = new Models.Medication.Queries.GetMedicationQuery.GetMedicationQuery()
            {
                Code = "AA"
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Code);
        }
    }
}
