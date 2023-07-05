using System;
using System.IO;
using DronesWebApi.Commons.Configuration;
using DronesWebApi.Core;
using DronesWebApi.Models.Image.Commands.UploadImageCommand;
using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace DronesWebApi.UnitTests.Validators.Image.Commands.UploadImageCommand
{
    public class UploadImageCommandValidatorTester 
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IOptions<UploadFileOptions>> _uploadFileOptions;
        private readonly UploadImageCommandValidator _validator;
        private readonly Mock<IFormFile> _formFile;

        public UploadImageCommandValidatorTester()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _uploadFileOptions = new Mock<IOptions<UploadFileOptions>>();

            _uploadFileOptions.Setup(x => x.Value).Returns(new UploadFileOptions());

            _formFile = new Mock<IFormFile>();

            _validator = new UploadImageCommandValidator(_unitOfWork.Object, _uploadFileOptions.Object)
                {
                    ClassLevelCascadeMode = CascadeMode.Stop,
                    RuleLevelCascadeMode = CascadeMode.Stop
                };
        }

        [Fact]
        public void Should_throw_error_when_MedicationCode_is_null()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            var model = new Models.Image.Commands.UploadImageCommand.UploadImageCommand()
            {
                MedicationCode = null
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.MedicationCode);
        }

        [Fact]
        public void Should_throw_error_when_MedicationCode_is_empty()
        {
            var model = new Models.Image.Commands.UploadImageCommand.UploadImageCommand()
            {
                MedicationCode = "  "
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.MedicationCode);
        }

        [Fact]
        public void Should_not_throw_error_when_MedicationCode_is_not_empty_nor_null()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            var model = new Models.Image.Commands.UploadImageCommand.UploadImageCommand()
            {
                MedicationCode = "a"
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.MedicationCode);
        }

        [Fact]
        public void Should_throw_error_when_Medication_does_not_exists_in_database()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns((Core.Domain.Medication)null);

            var model = new Models.Image.Commands.UploadImageCommand.UploadImageCommand()
            {
                MedicationCode = "a"
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.MedicationCode);
        }

        [Fact]
        public void Should_not_throw_error_when_Medication_exists()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            var model = new Models.Image.Commands.UploadImageCommand.UploadImageCommand()
            {
                MedicationCode = "a"
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.MedicationCode);
        }

        [Fact]
        public void Should_throw_error_when_File_is_null()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            var model = new Models.Image.Commands.UploadImageCommand.UploadImageCommand()
            {
                MedicationCode = "a",
                File = null
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.File);
        }

        [Fact]
        public void Should_throw_error_when_FileName_is_not_included_in_allowed_formats()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            _formFile.Setup(x => x.FileName).Returns("FileName.ext");

            var model = new Models.Image.Commands.UploadImageCommand.UploadImageCommand()
            {
                MedicationCode = "a",
                File = _formFile.Object
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.File);
        }

        [Fact]
        public void Should_not_throw_error_when_FileName_is_included_in_allowed_formats()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            _formFile.Setup(x => x.FileName)
                .Returns($"FileName{_uploadFileOptions.Object.Value.AllowedFormats[new Random().Next(0, _uploadFileOptions.Object.Value.AllowedFormats.Count)]}");

            var model = new Models.Image.Commands.UploadImageCommand.UploadImageCommand()
            {
                MedicationCode = "a",
                File = _formFile.Object
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.File);
        }

        [Fact]
        public void Should_throw_error_when_File_length_is_greater_than_MaxFileSizeInBytes()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            _formFile.Setup(x => x.FileName)
                .Returns($"FileName{_uploadFileOptions.Object.Value.AllowedFormats[new Random().Next(0, _uploadFileOptions.Object.Value.AllowedFormats.Count)]}");

            _formFile.Setup(x => x.Length).Returns(_uploadFileOptions.Object.Value.MaxFileSizeInBytes + 1);

            var model = new Models.Image.Commands.UploadImageCommand.UploadImageCommand()
            {
                MedicationCode = "a",
                File = _formFile.Object
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.File);
        }

        [Fact]
        public void Should_not_throw_error_when_File_length_is_less_or_equals_to_MaxFileSizeInBytes()
        {
            _unitOfWork.Setup(x => x.Medications.Get(It.IsAny<object[]>())).Returns(new Core.Domain.Medication());

            _formFile.Setup(x => x.FileName)
                .Returns($"FileName{_uploadFileOptions.Object.Value.AllowedFormats[new Random().Next(0, _uploadFileOptions.Object.Value.AllowedFormats.Count)]}");

            _formFile.Setup(x => x.Length).Returns(new Random().Next(1, _uploadFileOptions.Object.Value.MaxFileSizeInBytes + 1));

            var model = new Models.Image.Commands.UploadImageCommand.UploadImageCommand()
            {
                MedicationCode = "a",
                File = _formFile.Object
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.File);
        }
    }
}
