using DronesWebApi.Core;

namespace DronesWebApi.UnitTests.Validators.Image.Queries.DownloadImageQuery
{
    public class DownloadImageQueryValidatorTester
    {
        public DownloadImageQueryValidatorTester(IUnitOfWork unitOfWork)
        {
            //RuleFor(query => query.MedicationCode)
            //    .Must(medicationCode =>
            //    {
            //        var medication = unitOfWork.Medications.Get(medicationCode);

            //        return medication != null;

            //    }).WithMessage(command => $"Medication with code: '{command.MedicationCode}' does not exists")

            //    .Must(medicationCode =>
            //    {
            //        var image = unitOfWork.Images.GetImageOfMedication(medicationCode);

            //        return image != null;

            //    }).WithMessage(command => $"Medication with code: '{command.MedicationCode}' does not contains any image");
        }
    }
}
