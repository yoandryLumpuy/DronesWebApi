using System.Collections.Generic;

namespace DronesWebApi.Models.Medication.Commands.LoadMedicationListCommand
{
    public class LoadMedicationListCommandResponse
    {
        public IEnumerable<CreateMedicationCommand.CreateMedicationCommandResponse> Responses { get; set; }
    }
}
