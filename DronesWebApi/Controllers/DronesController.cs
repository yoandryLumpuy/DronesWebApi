using DronesWebApi.Models.Drone.Commands.CreateDroneCommand;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DronesWebApi.Commons.Models;

namespace DronesWebApi.Controllers
{
    public class DronesController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(Result<>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateAsync(CreateDroneCommand request, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request, cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAsync(CreateDroneCommand request, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request, cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }
    }
}
