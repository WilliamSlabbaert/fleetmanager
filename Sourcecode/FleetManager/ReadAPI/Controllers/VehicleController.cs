using BusinessLayer;
using BusinessLayer.managers.interfaces;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.validators.response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadAPI.Controllers
{
    [ApiController]
    public class VehicleController : Controller
    {
        private readonly ILogger<VehicleController> _logger;
        private IMediator _mediator;
        private readonly IVehicleService vehi;
        public VehicleController(ILogger<VehicleController> logger, IMediator mediator, IVehicleService vehi)
        {
            _logger = logger;
            _mediator = mediator;
            this.vehi = vehi;
        }
        [HttpGet("Vehicle")]
        public ActionResult<GenericResult> GetAllVehicles([FromQuery] GenericParameter parameter)
        {
            try
            {
                var temp = _mediator.Send(new GetVehiclesPagingQuery(parameter));
                return (temp.Result.StatusCode == 200) ? Ok(temp.Result) : BadRequest(temp.Result);
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
                var temp = _mediator.Send(new GetVehicleByIdQuery(id));
                return (temp.Result.StatusCode == 200) ? Ok(temp.Result) : NotFound(temp.Result);
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
                var temp = _mediator.Send(new GetVehicleChaffeursQuery(id));
                return (temp.Result.StatusCode == 200) ? Ok(temp.Result) : NotFound(temp.Result);
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
                var temp = _mediator.Send(new GetVehicleLicensePlatesQuery(id));
                return (temp.Result.StatusCode == 200) ? Ok(temp.Result) : NotFound(temp.Result);
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
                var temp = _mediator.Send(new GetVehicleRequestsQuery(id));
                return (temp.Result.StatusCode == 200) ? Ok(temp.Result) : NotFound(temp.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Vehicle/{id}/KilometerHistory")]
        public ActionResult<GenericResult> GetVehicleKilometersByID(int id)
        {
            try
            {
                var temp = _mediator.Send(new GetVehicleKilometerHistoryQuery(id));
                return (temp.Result.StatusCode == 200) ? Ok(temp.Result) : NotFound(temp.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
