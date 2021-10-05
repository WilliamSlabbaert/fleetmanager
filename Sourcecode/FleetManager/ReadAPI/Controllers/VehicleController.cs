using BusinessLayer;
using BusinessLayer.managers.interfaces;
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
        public ActionResult<GenericResult> GetAllVehicles()
        {
            try
            {
                return Ok(_mediator.Send(new GetVehiclesQuery()).Result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("Vehicle/{id}")]
        public ActionResult<GenericResult> GetVehicleByID(int id)
        {
            try
            {
                var vh = _mediator.Send(new GetVehicleByIdQuery(id));
                return Ok(vh.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                var result = (Vehicle)vh.Result.ReturnValue;
                return Ok(result.ChaffeurVehicles.Select(s => s.Chaffeur));
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
                var result = (Vehicle)vh.Result.ReturnValue;
                return Ok(result.LicensePlates);
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
    }
}
