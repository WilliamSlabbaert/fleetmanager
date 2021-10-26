using BusinessLayer.services.interfaces;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
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
        public ActionResult<GenericResult<GeneralModels>> GetAll([FromQuery] GenericParameter parameter)
        {
            try
            {
                var temp = _managerRequest.GetAllRequestsPaging(parameter);
                var metadata = _managerRequest.GetHeaders(parameter);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Request/{id}")]
        public ActionResult<GenericResult<GeneralModels>> GetById(int id)
        {
            try
            {
                var temp = _managerRequest.GetRequestById(id);
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Request/{id}/Maintenance")]
        public ActionResult<GenericResult<GeneralModels>> GetByIdMaintenance(int id)
        {
            try
            {
                var temp = _managerRequest.GetRequestMaintenance(id);
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Request/{id}/Repairment")]
        public ActionResult<GenericResult<GeneralModels>> GetByIdRepairments(int id)
        {
            try
            {
                var temp = _managerRequest.GetRequestRepairs(id);
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Request/{id}/Chaffeur")]
        public ActionResult<GenericResult<GeneralModels>> GetByIdChaffeur(int id)
        {
            try
            {
                var temp = _managerRequest.GetRequestChaffeur(id);
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Request/{id}/Vehicle")]
        public ActionResult<GenericResult<GeneralModels>> GetByIdVehicle(int id)
        {
            try
            {
                var temp = _managerRequest.GetRequestVehicle(id);
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
