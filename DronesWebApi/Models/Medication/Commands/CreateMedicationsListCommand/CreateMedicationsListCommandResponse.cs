using System.Collections.Generic;
using DronesWebApi.Models.Medication.Commands.CreateMedicationCommand;

namespace DronesWebApi.Models.Medication.Commands.CreateMedicationsListCommand
{
    public class CreateMedicationsListCommandResponse
    {
        public IEnumerable<CreateMedicationCommandResponse> Responses { get; set; }
    }
}
