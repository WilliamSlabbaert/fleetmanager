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
    public class MaintenanceController : Controller
    {
        private readonly ILogger<MaintenanceController> _logger;
        private IMaintenanceService _managerMaintenance;
        private IRequestService _managerRequest;
        public MaintenanceController(ILogger<MaintenanceController> logger, IMaintenanceService man, IRequestService managerRequest)
        {
            _logger = logger;
            _managerMaintenance = man;
            _managerRequest = managerRequest;
        }
        [HttpGet("Maintenance")]
        public ActionResult<List<Maintenance>> Getall()
        {
            try
            {
                var temp = _managerMaintenance.GetAllMaintenances();
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("Maintenance")]
        public ActionResult Add()
        {
            try
            {
                var temp = new Maintenance(DateTime.Now, 123, "testGarage");
                if (_managerMaintenance.ValidateMaintance(temp) == false)
                {
                    return BadRequest(_managerMaintenance._errors);
                }
                else
                {
                    var result = _managerMaintenance.AddMaintenance(temp, 1);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Maintenance/{id}")]
        public ActionResult<Maintenance> GetById(int id)
        {
            try
            {
                var vh = _managerMaintenance.GetMaintenanceById(id);
                if (vh == null)
                {
                    return NotFound("This Maintenance doesn't exist");
                }
                return Ok(vh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Maintenance/{id}/Request")]
        public ActionResult<Request> GetByIdRequest(int id)
        {
            try
            {
                var vh = _managerMaintenance.GetMaintenanceById(id);
                if (vh == null)
                {
                    return NotFound("This Maintenance doesn't exist");
                }
                return Ok(vh.Request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("Maintenance/{id}/Request/{requestId}")]
        public ActionResult<Request> UpdateMaintenance(int id, int requestId)
        {
            try
            {
                var vh = _managerMaintenance.GetMaintenanceById(id);
                if (vh == null)
                {
                    return NotFound("This Maintenance doesn't exist");
                }
                else
                {
                    var rq = _managerRequest.GetRequestById(requestId);
                    if(rq == null)
                    {
                        return NotFound("This request doesn't exist");
                    }
                    else
                    {
                        var temp = new Maintenance(DateTime.Now, 123, "testGarage2");
                        if (_managerMaintenance.ValidateMaintance(temp) == false)
                        {
                            return BadRequest(_managerMaintenance._errors);
                        }
                        else{
                            var result = _managerMaintenance.UpdateMaintenance(temp,maintenanceId: id,requestId: requestId);
                            return Ok(result);
                        }
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
