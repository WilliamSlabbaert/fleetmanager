using BusinessLayer.services.interfaces;
using BusinessLayer.models;
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
    public class RepairmentController : Controller
    {
        private readonly ILogger<RepairmentController> _logger;
        private IRepairmentService _managerRepairment;
        private IRequestService _managerRequest;
        public RepairmentController(ILogger<RepairmentController> logger, IRepairmentService man, IRequestService managerRequest)
        {
            _logger = logger;
            _managerRepairment = man;
            _managerRequest = managerRequest;
        }
        [HttpGet("Repairment")]
        public ActionResult<List<Repairment>> Getall([FromQuery] GenericParameter parameter)
        {
            try
            {
                var temp = _managerRepairment.GetAllRepairmentsPaging(parameter);
                var metadata = _managerRepairment.GetHeaders(parameter);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        

        [HttpGet("Repairment/{id}")]
        public ActionResult<Repairment> GetById(int id)
        {
            try
            {
                var temp = _managerRepairment.GetRepairmentById(id);
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
