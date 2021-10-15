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
    public class MaintenanceController : Controller
    {
        private readonly ILogger<ChaffeurController> _logger;
        private IMediator _mediator;
        private IRequestService _requestService;
        private IMaintenanceService _maintenanceService;
        private IRepairmentService _repairmentService;
        public MaintenanceController(ILogger<ChaffeurController> logger, IMediator mediator, IRequestService requestService, IMaintenanceService maintenanceService, IRepairmentService repairmentService)
        {
            _logger = logger;
            _mediator = mediator;
            _requestService = requestService;
            _maintenanceService = maintenanceService;
            _repairmentService = repairmentService;
        }
        [HttpPut("Maintenance/{id}")]
        public ActionResult<GenericResult<IGeneralModels>> UpdateMaintenance(int id, [FromBody] Maintenance maintenance)
        {
            try
            {
                var check = _maintenanceService.GetMaintenanceById(id);
                if (check.StatusCode != 200)
                {
                    return NotFound(check);
                }
                var result = _maintenanceService.UpdateMaintenance(id,maintenance);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Maintenance/{id}/Invoices")]
        public ActionResult<GenericResult<IGeneralModels>> AddInvoice(int id, [FromBody] Invoice invoice)
        {
            try
            {
                var check = _maintenanceService.GetMaintenanceById(id);
                if (check.StatusCode != 200)
                {
                    return NotFound(check);
                }
                var result = _maintenanceService.AddInvoice(id, invoice);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Maintenance/{maintenanceId}/Invoices/{invoiceId}")]
        public ActionResult<GenericResult<IGeneralModels>> AddInvoice(int maintenanceId, int invoiceId)
        {
            try
            {
                var check = _maintenanceService.GetMaintenanceById(maintenanceId);
                if (check.StatusCode != 200)
                {
                    return NotFound(check);
                }
                var result = _maintenanceService.DeleteInvoice(maintenanceId, invoiceId);
                return result.StatusCode == 200 ? Ok(result) : NotFound(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
