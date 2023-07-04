using DronesWebApi.Commons.Models;
using DronesWebApi.Models.Drone.Queries.GetAllAvailableDronesForLoadingQuery;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using DronesWebApi.Models.Medication.Commands.CreateMedicationCommand;
using DronesWebApi.Models.Medication.Commands.CreateMedicationsListCommand;
using DronesWebApi.Models.Medication.Commands.LoadImageCommand;
using DronesWebApi.Models.Medication.Commands.LoadMedicationCommand;
using DronesWebApi.Models.Medication.Commands.LoadMedicationListCommand;
using DronesWebApi.Models.Medication.Queries.GetLoadedMedicationsQuery;
using DronesWebApi.Models.Medication.Queries.GetMedicationQuery;
using DronesWebApi.Models.Medication.Queries.GetNotLoadedRegisteredMedicationsQuery;
using Microsoft.AspNetCore.Http;

namespace DronesWebApi.Controllers
{
    public class MedicationsController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateAsync(CreateMedicationCommand request, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request, cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }

        [HttpPost("by-list")]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateByListAsync(CreateMedicationsListCommand request, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request, cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }

        [HttpPost(template: "load")]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> LoadAsync(LoadMedicationCommand request, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request, cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }

        [HttpPost(template: "load/by-list")]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> LoadMultiplesAsync(LoadMedicationListCommand request, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request, cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }

        [HttpGet(template: "{code}")]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAsync([FromRoute] string code, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request: new GetMedicationQuery(){ Code = code }, cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }

        [HttpGet(template: "loaded/{droneId}")]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetLoadedAsync([FromRoute] int droneId, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request: new GetLoadedMedicationsQuery() { DroneId = droneId }, cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }

        [HttpGet(template: "not-loaded")]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetNotLoadedAsync(CancellationToken cancellation)
        {
            var result = await Mediator.Send(request: new GetNotLoadedRegisteredMedicationsQuery(), cancellation).ConfigureAwait(false);

            return Ok(Result.Success(result));
        }

        [HttpGet(template: "Image/{medicationCode}")]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Result<>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PostImageAsync([FromRoute] string medicationCode, IFormFile file, CancellationToken cancellation)
        {
            var result = await Mediator.Send(request: new LoadImageCommand(){ File = file, MedicationCode = medicationCode }, cancellation)
                .ConfigureAwait(false);

            return Ok(Result.Success(result));
        }
    }
}
