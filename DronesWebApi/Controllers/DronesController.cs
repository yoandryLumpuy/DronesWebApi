using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using DronesWebApi.Commons.Responses;
using DronesWebApi.Models.Drone.Commands.CreateDroneCommand;

namespace DronesWebApi.Controllers
{
    public class DronesController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse<>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateAsync(CreateDroneCommand request, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request, cancellation).ConfigureAwait(false);

            return Ok(new SuccessResponse<CreateCommandResponse>(result));
        }
    }
}
