using BusinessLayer;
using BusinessLayer.managers.interfaces;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private readonly IVehicleService _repo;
        public VehicleController(ILogger<VehicleController> logger, IMediator mediator, IVehicleService repo)
        {
            _logger = logger;
            _mediator = mediator;
            this._repo = repo;
        }
        [HttpGet("Vehicle")]
        public ActionResult<GenericResult<GeneralModels>> GetAllVehicles([FromQuery] GenericParameter parameter)
        {
            try
            {
                var temp = _mediator.Send(new GetVehiclesPagingQuery(parameter));
                var metadata = _repo.GetHeaders(parameter);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return (temp.Result.StatusCode == 200) ? Ok(temp.Result) : BadRequest(temp.Result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Vehicle/{id}")]
        public ActionResult<GenericResult<GeneralModels>> GetVehicleByID(int id)
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
        [HttpGet("Vehicle/{id}/Chaffeur")]
        public ActionResult<GenericResult<GeneralModels>> GetVehicleChaffeursByID(int id)
        {
            try
            {
                var temp = _mediator.Send(new GetVehicleChauffeursQuery(id));
                return (temp.Result.StatusCode == 200) ? Ok(temp.Result) : NotFound(temp.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Vehicle/{id}/Licenseplate")]
        public ActionResult<GenericResult<GeneralModels>> GetVehicleLicensePlatesByID(int id)
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
        [HttpGet("Vehicle/{id}/Request")]
        public ActionResult<GenericResult<GeneralModels>> GetVehicleRequestsByID(int id)
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
        public ActionResult<GenericResult<GeneralModels>> GetVehicleKilometersByID(int id)
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
