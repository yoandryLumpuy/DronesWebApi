using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DronesWebApi.Models.Medication.Commands.CreateMedicationCommand;
using MediatR;

namespace DronesWebApi.Models.Medication.Commands.CreateMedicationsListCommand
{
    public class CreateMedicationsListCommand: IRequest<CreateMedicationsListCommandResponse>
    {
        public IEnumerable<CreateMedicationCommand.CreateMedicationCommand> Commands { get; set; }
    }

    public class CreateMedicationsListCommandHandler : IRequestHandler<CreateMedicationsListCommand, CreateMedicationsListCommandResponse>
    {
        private readonly ISender _mediator;

        public CreateMedicationsListCommandHandler(ISender mediator)
        {
            _mediator = mediator;
        }

        public async Task<CreateMedicationsListCommandResponse> Handle(CreateMedicationsListCommand request, CancellationToken cancellationToken)
        {
            var responses = new List<CreateMedicationCommandResponse>();

            foreach (var command in request.Commands)
            {
                var response = await _mediator.Send(command, cancellationToken);
                responses.Add(response);
            }

            return new CreateMedicationsListCommandResponse(){ Responses = responses };
        }
    }
}
