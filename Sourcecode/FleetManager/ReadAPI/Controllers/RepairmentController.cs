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
        private IRepairmentService _managerRepairment;
        private IRequestService _managerRequest;
        public RepairmentController(ILogger<RepairmentController> logger, IRepairmentService man, IRequestService managerRequest)
        {
            _logger = logger;
            _managerRepairment = man;
            _managerRequest = managerRequest;
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
        [HttpPost("Repairment")]
        public ActionResult Add()
        {
            try
            {
                var temp = _managerRepairment.AddRepairment(new Repairment(DateTime.Now, "testdes", "testcomp"), 1);
                if (_managerRepairment._errors.Count != 0)
                {
                    return BadRequest(_managerRepairment._errors);
                }
                else
                {
                    return Ok(temp);
                }
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
        [HttpPut("Repairment/{id}/Request/{requestId}")]
        public ActionResult<Repairment> UpdateById(int id, int requestId)
        {
            try
            {
                var vh = _managerRepairment.GetRepairmentById(id);
                if (vh == null)
                {
                    return NotFound("This repairment doesn't exist");
                }
                else
                {
                    var req = _managerRequest.GetRequestById(requestId);
                    if (req == null)
                    {
                        return NotFound("This request doesn't exist");
                    }
                    else
                    {
                        var result = _managerRepairment.UpdateRepairment(new Repairment(DateTime.Now, "test222", "comcom"), requestId,id);
                        return Ok(result);
                    }
                }
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
