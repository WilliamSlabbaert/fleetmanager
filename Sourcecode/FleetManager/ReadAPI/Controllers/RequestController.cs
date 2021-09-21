﻿using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadAPI.Controllers
{
    public class RequestController : Controller
    {
        private readonly ILogger<RequestController> _logger;
        private IRequestManager _managerRequest;
        public RequestController(ILogger<RequestController> logger, IRequestManager man)
        {
            _logger = logger;
            _managerRequest = man;
        }

        [HttpGet("Request")]
        public ActionResult<List<Request>> GetAll()
        {
            //_managerRequest.AddRequest(new Request(DateTime.Now,DateTime.Now,"test"),1,1);
            try
            {
                var temp = _managerRequest.GetAllRequests();
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Request/{id}")]
        public ActionResult<FuelCard> GetById(int id)
        {
            try
            {
                var vh = _managerRequest.GetRequestById(id);
                if (vh == null)
                {
                    return NotFound("This request doesn't exist");
                }
                return Ok(vh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Request/{id}/Maintenance")]
        public ActionResult<List<Maintenance>> GetByIdMaintenance(int id)
        {
            try
            {
                var vh = _managerRequest.GetRequestById(id);
                if (vh == null)
                {
                    return NotFound("This request doesn't exist");
                }
                return Ok(vh.Maintenance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Request/{id}/Repairments")]
        public ActionResult<List<Maintenance>> GetByIdRepairments(int id)
        {
            try
            {
                var vh = _managerRequest.GetRequestById(id);
                if (vh == null)
                {
                    return NotFound("This request doesn't exist");
                }
                return Ok(vh.Repairment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}