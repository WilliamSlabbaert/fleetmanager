using BusinessLayer;
using BusinessLayer.managers.interfaces;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadAPI.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ILogger<VehicleController> _logger;
        private IMediator _mediator;
        public VehicleController(ILogger<VehicleController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet("Vehicle")]
        public ActionResult<Vehicle> GetAllVehicles()
        {
            try
            {
                return Ok(_mediator.Send(new GetVehiclesQuery()).Result);
            }catch(Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("Vehicle")]
        public ActionResult<Vehicle> AddVehicle()
        {
            var temp = new Vehicle(13414, Overall.CarTypes.Passengercar, 191, Overall.FuelTypes.Electric, null, "A3", DateTime.Now);
            var check = _mediator.Send(new CheckExistingVehicleQuery(temp));
            if (check.Result)
            {
                var command = new AddVehicleCommand(temp);
                _mediator.Send(command);
                if (command._errors.Count != 0)
                {
                    return BadRequest(command._errors);
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                return BadRequest("Vehicle with chassis number already exist.");
            }
        }
        [HttpGet("Vehicle/{id}")]
        public ActionResult<Vehicle> GetVehicleByID(int id)
        {
            try
            {
                var vh = _mediator.Send(new GetVehicleByIdQuery(id));
                if (vh == null)
                {
                    return NotFound("This vehicle doesn't exist");
                }
                return Ok(vh.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Vehicle/{id}/Chaffeurs")]
        public ActionResult<List<Chaffeur>> GetVehicleChaffeursByID(int id)
        {
            try
            {
                var vh = _mediator.Send(new GetVehicleByIdQuery(id));
                if (vh == null)
                {
                    return NotFound("This vehicle doesn't exist");
                }
                return Ok(vh.Result.ChaffeurVehicles.Select(s => s.Chaffeur));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Vehicle/{id}/Licenseplates")]
        public ActionResult<List<Chaffeur>> GetVehicleLicensePlatesByID(int id)
        {
            try
            {
                var vh = _mediator.Send(new GetVehicleByIdQuery(id));
                if (vh == null)
                {
                    return NotFound("This vehicle doesn't exist");
                }
                return Ok(vh.Result.LicensePlates);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("Vehicle/{id}/Licenseplates")]
        public ActionResult<List<Chaffeur>> AddLicenseplateToVehicle(int id)
        {
            try
            {
                var plate = new LicensePlate("Test");
                var command = new CheckExistingLicensePlateQuery(plate);
                if(_mediator.Send(command).Result)
                {
                    var command2 = new AddLicensePlateToVehicleCommand(id, plate);
                    _mediator.Send(command2);
                    if (command2._errors.Count != 0)
                    {
                        return BadRequest(command2._errors);
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest("Licenseplate already exists.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
