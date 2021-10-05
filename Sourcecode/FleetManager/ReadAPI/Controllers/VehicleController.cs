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
        public ActionResult<GenericResult> GetVehicleChaffeursByID(int id)
        {
            try
            {
                var vh = _mediator.Send(new GetVehicleChaffeursQuery(id));
                return Ok(vh.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Vehicle/{id}/Licenseplates")]
        public ActionResult<GenericResult> GetVehicleLicensePlatesByID(int id)
        {
            try
            {
                var vh = _mediator.Send(new GetVehicleLicensePlatesQuery(id));
                return Ok(vh.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Vehicle/{id}/Requests")]
        public ActionResult<GenericResult> GetVehicleRequestsByID(int id)
        {
            try
            {
                var vh = _mediator.Send(new GetVehicleRequestsQuery(id));
                return Ok(vh.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
