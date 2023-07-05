using System.IO;
using DronesWebApi.Commons.Configuration;
using DronesWebApi.Core;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace DronesWebApi.Models.Image.Commands.UploadImageCommand
{
    public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
    {
        public UploadImageCommandValidator(IUnitOfWork unitOfWork, IOptions<UploadFileOptions> uploadFileOptions)
        {
            var uploadFileConfig = uploadFileOptions?.Value ?? new UploadFileOptions();

            RuleFor(x => x.MedicationCode)
                .NotNull().WithMessage("MedicationCode field can't be null")
                .NotEmpty().WithMessage("MedicationCode field can not be empty")
                .Must(medicationCode =>
                {
                    var medication = unitOfWork.Medications.Get(medicationCode);

                    return medication != null;

                }).WithMessage(command => $"Medication item with code: '{command.MedicationCode}' does not exists");


            RuleFor(x => x.File)
                .NotNull().WithMessage("File field can't be null")
                .Must(file => uploadFileConfig.AllowedFormats.Contains(Path.GetExtension(file.FileName)))
                .WithMessage($"Allowed image types are: '{string.Join(",", uploadFileConfig.AllowedFormats)}'")

                .Must(file => file.Length <= uploadFileConfig.MaxFileSizeInBytes)
                .WithMessage($"Maximum file size is set to: {uploadFileConfig.MaxFileSizeInBytes} bytes");
        }
    }
}
