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
        public MaintenanceController(ILogger<MaintenanceController> logger, IMaintenanceService man)
        {
            _logger = logger;
            _managerMaintenance = man;
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
                _managerMaintenance.AddMaintenance(new Maintenance(DateTime.Now,123,"testGarage"), 1);
                if (_managerMaintenance._errors.Count != 0)
                {
                    return BadRequest(_managerMaintenance._errors);
                }
                else
                {
                    return Ok();
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
    }
}
