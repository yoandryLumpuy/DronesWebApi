using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DronesWebApi.Models.Medication.Commands.CreateMedicationCommand;
using DronesWebApi.Models.Medication.Commands.CreateMedicationsListCommand;
using MediatR;

namespace DronesWebApi.Models.Medication.Commands.LoadMedicationListCommand
{
    public class LoadMedicationListCommand: IRequest<LoadMedicationListCommandResponse>
    {
        public IEnumerable<LoadMedicationCommand.LoadMedicationCommand> Commands { get; set; }
    }

    public class LoadMedicationListCommandHandler : IRequestHandler<LoadMedicationListCommand, LoadMedicationListCommandResponse>
    {
        private readonly ISender _mediator;

        public LoadMedicationListCommandHandler(ISender mediator)
        {
            _mediator = mediator;
        }

        public async Task<LoadMedicationListCommandResponse> Handle(LoadMedicationListCommand request, CancellationToken cancellationToken)
        {
            var responses = new List<CreateMedicationCommandResponse>();

            foreach (var command in request.Commands)
            {
                var response = await _mediator.Send(command, cancellationToken);
                responses.Add(response);
            }

            return new LoadMedicationListCommandResponse() { Responses = responses };
        }
    }
}
