using BusinessLayer;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
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
        public async Task<ActionResult<Vehicle>> AddVehicle([FromBody] Vehicle vehicle)
        {
            try
            {
                var temp = new GenericResult();
                temp.SetStatusCode(Overall.ResponseType.BadRequest);
                var result = await _mediator.Send(new AddVehicleCommand(vehicle));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Vehicle/{id}/Licenseplates")]
        public ActionResult<List<Chaffeur>> AddLicenseplateToVehicle(int id)
        {
            try
            {
                var plate = new LicensePlate("Test6", true);
                var command = new CheckExistingLicensePlateQuery(plate);
                var vh = _mediator.Send(new GetVehicleByIdQuery(id));
                if (vh.Result == null)
                {
                    return NotFound("This vehicle doesn't exist");
                }
                else
                {
                    if (_mediator.Send(command).Result)
                    {
                        var command2 = new AddLicensePlateToVehicleCommand(id, plate);
                        var result = _mediator.Send(command2);
                        if (command2._errors.Count != 0)
                        {
                            return BadRequest(command2._errors);
                        }
                        return Ok(result.Result);
                    }
                    else
                    {
                        return BadRequest("Licenseplate already exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("Vehicle/{id}/Licenseplates/{licenseId}")]
        public ActionResult<List<Chaffeur>> PutLicenseplateToVehicle(int id, int licenseId)
        {
            try
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
