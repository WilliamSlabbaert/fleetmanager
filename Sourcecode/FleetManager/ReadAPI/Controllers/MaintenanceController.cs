using BusinessLayer.managers.interfaces;
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
        public ActionResult<GenericResult<IGeneralModels>> Getall([FromQuery] GenericParameter parameter)
        {
            try
            {
                //_managerMaintenance.AddMaintenance(new Maintenance(DateTime.Now,123,"test"),1);
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
        public ActionResult<GenericResult<IGeneralModels>> GetById(int id)
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
        [HttpGet("Maintenance/{id}/Request")]
        public ActionResult<GenericResult<IGeneralModels>> GetByIdRequest(int id)

        {
            try
            {
                var temp = _managerMaintenance.GetMaintenanceRequestById(id);
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Maintenance/{id}/Invoices")]
        public ActionResult<GenericResult<IGeneralModels>> GetByIdInvoices(int id)
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
        /*
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
        }*/
    }
}
