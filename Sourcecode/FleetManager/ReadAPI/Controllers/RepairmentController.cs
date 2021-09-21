using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadAPI.Controllers
{
    public class RepairmentController : Controller
    {
        private readonly ILogger<RepairmentController> _logger;
        private IRepairmentManager _managerRepairment;
        public RepairmentController(ILogger<RepairmentController> logger, IRepairmentManager man)
        {
            _logger = logger;
            _managerRepairment = man;
        }
        [HttpGet("Repairment")]
        public ActionResult<List<Repairment>> Getall()
        {
            try
            {
                var temp = _managerRepairment.GetAllRepairments();
                return Ok(temp);
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
                var vh = _managerRepairment.GetRepairmentById(id);
                if (vh == null)
                {
                    return NotFound("This repairment doesn't exist");
                }
                return Ok(vh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Repairment/{id}/Request")]
        public ActionResult<Request> GetByIdRequest(int id)
        {
            try
            {
                var vh = _managerRepairment.GetRepairmentById(id);
                if (vh == null)
                {
                    return NotFound("This repairment doesn't exist");
                }
                return Ok(vh.Request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
