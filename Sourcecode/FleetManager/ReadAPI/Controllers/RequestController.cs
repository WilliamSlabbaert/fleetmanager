using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using BusinessLayer.validators.response;
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
    public class RequestController : Controller
    {
        private readonly ILogger<RequestController> _logger;
        private IRequestService _managerRequest;
        public RequestController(ILogger<RequestController> logger, IRequestService man)
        {
            _logger = logger;
            _managerRequest = man;
        }

        [HttpGet("Request")]
        public ActionResult<GenericResult> GetAll([FromQuery] GenericParameter parameter)
        {
            //_managerRequest.AddRequest(new Request(DateTime.Now,DateTime.Now,"test",Overall.RequestType.Fuelcard),2,1);
            try
            {
                var temp = _managerRequest.GetAllRequestsPaging(parameter);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Request/{id}")]
        public ActionResult<GenericResult> GetById(int id)
        {
            try
            {
                var vh = _managerRequest.GetRequestById(id);
                return Ok(vh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Request/{id}/Maintenances")]
        public ActionResult<GenericResult> GetByIdMaintenance(int id)
        {
            try
            {
                var vh = _managerRequest.GetRequestMaintenance(id);
                return Ok(vh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Request/{id}/Repairments")]
        public ActionResult<GenericResult> GetByIdRepairments(int id)
        {
            try
            {
                var vh = _managerRequest.GetRequestRepairs(id);
                return Ok(vh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Request/{id}/Chaffeur")]
        public ActionResult<GenericResult> GetByIdChaffeur(int id)
        {
            try
            {
                var vh = _managerRequest.GetRequestChaffeur(id);
                return Ok(vh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Request/{id}/Vehicle")]
        public ActionResult<GenericResult> GetByIdVehicle(int id)
        {
            try
            {
                var vh = _managerRequest.GetRequestVehicle(id);
                return Ok(vh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /*
        [HttpPost("Request")]
        public ActionResult Add()
        {
            var result = _managerRequest.AddRequest(new Request(DateTime.Now, DateTime.Now, "test",Overall.RequestType.Fuelcard), 1, 1);

            if(_managerRequest._errors.Count != 0)
            {
                return BadRequest(_managerRequest._errors);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPut("Request/{id}")]
        public ActionResult Update(int id)
        {
            var result = _managerRequest.UpdateRequest(new Request(DateTime.Now, DateTime.Now, "test2", Overall.RequestType.Fuelcard),vehicleid: 1,chaffeurid: 2, id: id);
            if (_managerRequest._errors.Count != 0)
            {
                return BadRequest(_managerRequest._errors);
            }
            else
            {
                return Ok(result);
            }
        }*/
    }
}
