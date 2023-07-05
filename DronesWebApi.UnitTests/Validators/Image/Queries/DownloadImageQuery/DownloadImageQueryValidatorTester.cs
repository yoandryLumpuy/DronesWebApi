using DronesWebApi.Core;
using DronesWebApi.Models.Image.Queries.DownloadImageQuery;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.Image.Queries.DownloadImageQuery
{
    public class DownloadImageQueryValidatorTester
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly DownloadImageQueryValidator _validator;

        public DownloadImageQueryValidatorTester()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _validator = new DownloadImageQueryValidator(_unitOfWork.Object)
            {
                RuleLevelCascadeMode = CascadeMode.Stop,
                ClassLevelCascadeMode = CascadeMode.Stop
            };
        }

        [Fact]
        public void Should_throw_error_when_Medication_does_not_exists()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns((Core.Domain.Medication)null);

            var model = new Models.Image.Queries.DownloadImageQuery.DownloadImageQuery()
            {
                MedicationCode = "AAA1"
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.MedicationCode);
        }

        [Fact]
        public void Should_throw_error_when_ImageForMedication_does_not_exists()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            _unitOfWork.Setup(x => x.Images.GetImageOfMedication(It.IsAny<string>())).Returns((Core.Domain.Image)null);

            var model = new Models.Image.Queries.DownloadImageQuery.DownloadImageQuery()
            {
                MedicationCode = "AAA1"
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.MedicationCode);
        }

        [Fact]
        public void Should_not_throw_error_when_Medication_and_ImageOfMedication_exists()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            _unitOfWork.Setup(x => x.Images.GetImageOfMedication(It.IsAny<string>())).Returns(new Core.Domain.Image());

            var model = new Models.Image.Queries.DownloadImageQuery.DownloadImageQuery()
            {
                MedicationCode = "AAA1"
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.MedicationCode);
        }
    }
}
