﻿using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAPI.Controllers
{
    [ApiController]
    public class RequestController : Controller
    {
        private readonly ILogger<ChaffeurController> _logger;
        private IMediator _mediator;
        private IRequestService _requestService;
        private IMaintenanceService _maintenanceService;
        private IRepairmentService _repairmentService;
        public RequestController(ILogger<ChaffeurController> logger, IMediator mediator, IRequestService requestService, IMaintenanceService maintenanceService, IRepairmentService repairmentService)
        {
            _logger = logger;
            _mediator = mediator;
            _requestService = requestService;
            _maintenanceService = maintenanceService;
            _repairmentService = repairmentService;
        }

        [HttpPut("Request/{id}")]
        public ActionResult<GenericResult<IGeneralModels>> UpdateRequest(int id,[FromBody] Request request)
        {
            try
            {
                var check = _requestService.GetRequestById(id);
                if(check.StatusCode != 200)
                {
                    return NotFound(check);
                }
                var result = _requestService.UpdateRequest(request,id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Request/{id}/Maintenances")]
        public ActionResult<GenericResult<IGeneralModels>> AddMaintenance(int id, [FromBody] Maintenance maintenance)
        {
            try
            {
                var check = _requestService.GetRequestById(id);
                if (check.StatusCode != 200)
                {
                    return NotFound(check);
                }
                var result = _maintenanceService.AddMaintenance(maintenance,id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Request/{requestId}/Maintenances/{maintenanceId}")]
        public ActionResult<GenericResult<IGeneralModels>> DeleteMaintenance(int requestId, int maintenanceId)
        {
            try
            {
                var check = _requestService.GetRequestById(requestId);
                var check2 = _maintenanceService.GetMaintenanceById(maintenanceId);
                if (check.StatusCode != 200 || check2.StatusCode != 200)
                {
                    return check.StatusCode != 200 ? NotFound(check) : NotFound(check2);
                }
                var result = _maintenanceService.DeleteMaintenance(requestId, maintenanceId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Request/{id}/Repairments")]
        public ActionResult<GenericResult<IGeneralModels>> AddRepair(int id, [FromBody] Repairment repairment)
        {
            try
            {
                var check = _requestService.GetRequestById(id);
                if (check.StatusCode != 200)
                {
                    return NotFound(check);
                }
                var result = _repairmentService.AddRepairment(repairment, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Request/{requestId}/Repairments/{repairmentId}")]
        public ActionResult<GenericResult<IGeneralModels>> AddRepair(int requestId,int repairmentId)
        {
            try
            {
                var check = _requestService.GetRequestById(requestId);
                var check2 = _repairmentService.GetRepairmentById(repairmentId);
                if (check.StatusCode != 200 || check2.StatusCode != 200)
                {
                    return check.StatusCode != 200 ? NotFound(check) : NotFound(check2);
                }
                var result = _repairmentService.DeleteRepairment(requestId,repairmentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
