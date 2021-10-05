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
            var temp = new Vehicle(1344, Overall.CarTypes.Passengercar, Overall.FuelTypes.Electric, "BMW", "1-Serie", DateTime.Now);
            var check = _mediator.Send(new CheckExistingVehicleQuery(temp));
            if (check.Result)
            {
                var command = new AddVehicleCommand(temp);
                var result = _mediator.Send(command);
                if (command._errors.Count != 0)
                {
                    return BadRequest(command._errors);
                }
                else
                {
                    return Ok(result.Result);
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
                if (vh.Result == null)
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
                if (vh.Result == null)
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
                if (vh.Result == null)
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
                var plate = new LicensePlate("Test6",true);
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
        [HttpGet("Vehicle/{id}/Licenseplates/{licenseId}")]
        public ActionResult<List<Chaffeur>> GetLicenseplateToVehicle(int id, int licenseId)
        {
            try
            {
                var vh = _mediator.Send(new GetVehicleByIdQuery(id));
                if (vh.Result == null)
                {
                    return NotFound("This vehicle doesn't exist");
                }
                else
                {
                    var lp = _mediator.Send(new GetLicensePlateFromVehicleQuery(id, licenseId));
                    if (lp.Result == null)
                    {
                        return NotFound("This licenseplate doesn't exist");
                    }
                    else
                    {
                        return Ok(lp.Result);
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
                var vh = _mediator.Send(new GetVehicleByIdQuery(id));
                if (vh.Result == null)
                {
                    return NotFound("This vehicle doesn't exist");
                }
                else
                {
                    var lp = _mediator.Send(new GetLicensePlateFromVehicleQuery(id, licenseId));
                    if (lp.Result == null)
                    {
                        return NotFound("This licenseplate doesn't exist");
                    }
                    else
                    {
                        var plate = new LicensePlate("TEST3", true) { Id = licenseId};
                        var command = new CheckExistingLicensePlateQuery(plate);
                        var command2 = new UpdateLicensePlateFromVehicleCommand(id,licenseId,plate);
                        var result = _mediator.Send(command);
                        if (result.Result)
                        {
                            var result2 = _mediator.Send(command2);
                            if (command2._errors.Count != 0)
                            {
                                return BadRequest(command2._errors);
                            }
                            else
                            {
                                return Ok(result2.Result);
                            }
                        }
                        else
                        {
                            return BadRequest("Licenseplate already exists.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
