using BusinessLayer;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAPI.Controllers
{
    [ApiController]
    public class VehicleController : Controller
    {
        private readonly ILogger<VehicleController> _logger;
        private IMediator _mediator;
        public VehicleController(ILogger<VehicleController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpPost("Vehicle")]
        public async Task<ActionResult<GenericResult<IGeneralModels>>> AddVehicle([FromBody] Vehicle vehicle)
        {
            try
            {
                var result = await _mediator.Send(new AddVehicleCommand(vehicle));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Vehicle/{id}")]
        public async Task<ActionResult<GenericResult<IGeneralModels>>> UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            try
            {

                var vh = _mediator.Send(new GetVehicleByIdQuery(id)).Result;
                if (vh.StatusCode != 200)
                {
                    return NotFound(vh);
                }

                var result = await _mediator.Send(new UpdateVehicleCommand(id,vehicle));
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Vehicle/{id}/Licenseplates")]
        public async Task<ActionResult<GenericResult<IGeneralModels>>> AddLicenseplateToVehicle(int id, [FromBody] LicensePlate licensePlate)
        {
            try
            {
                var vh = _mediator.Send(new GetVehicleByIdQuery(id)).Result;
                if (vh.StatusCode != 200)
                {
                    return NotFound(vh);
                }
                var result = await _mediator.Send(new AddLicensePlateToVehicleCommand(id, new LicensePlate(licensePlate.Plate)));
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("Vehicle/{id}/Licenseplates/{licenseId}")]
        public async Task<ActionResult<GenericResult<IGeneralModels>>> PutLicenseplateToVehicle(int id, int licenseId, [FromBody] LicensePlate licensePlate)
        {
            try
            {
                var vh = _mediator.Send(new GetVehicleByIdQuery(id)).Result;
                var lp = _mediator.Send(new GetLicensePlateByIdQuery(licenseId)).Result;
                if (vh.StatusCode != 200 || lp.StatusCode != 200)
                {
                    return vh.StatusCode != 200 ? NotFound(vh) : NotFound(lp);
                }
                var result = await _mediator.Send(new UpdateLicensePlateFromVehicleCommand(id, licenseId ,licensePlate));
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
