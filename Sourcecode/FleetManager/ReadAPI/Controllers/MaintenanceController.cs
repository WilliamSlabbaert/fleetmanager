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
        public ActionResult<GenericResult<GeneralModels>> Getall([FromQuery] GenericParameter parameter)
        {
            try
            {
                var temp = _managerMaintenance.GetAllMaintenancesPaging(parameter);
                var metadata = _managerMaintenance.GetHeaders(parameter);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Maintenance/{id}")]
        public ActionResult<GenericResult<GeneralModels>> GetById(int id)
        {
            try
            {
                var temp = _managerMaintenance.GetMaintenanceById(id);
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Maintenance/{id}/Invoice")]
        public ActionResult<GenericResult<GeneralModels>> GetByIdInvoices(int id)
        {
            try
            {
                var temp = _managerMaintenance.GetMaintenanceInvoicesById(id);
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
