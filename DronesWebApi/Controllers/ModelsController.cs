using DronesWebApi.Commons.Responses;
using DronesWebApi.Models.DroneModel.Commands.CreateDroneModelCommand;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DronesWebApi.Controllers
{
    public class ModelsController: ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateAsync(CreateDroneModelCommand request, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request, cancellation).ConfigureAwait(false);

            return Ok(new SuccessResponse<CreateDroneModelCommandResponse>(result));
        }
    }
}
