using BusinessLayer;
using BusinessLayer.managers.interfaces;
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
        public VehicleController(ILogger<VehicleController> logger, IChaffeurService man, IVehicleService man2, IMediator mediator)
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
    }
}
