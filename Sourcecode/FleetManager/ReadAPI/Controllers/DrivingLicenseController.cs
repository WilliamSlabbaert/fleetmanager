﻿using BusinessLayer;
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
    public class DrivingLicenseController : Controller
    {
        private readonly ILogger<DrivingLicenseController> _logger;
        private IChauffeurService _managerChaffeur;
        private IDrivingLicenseService _drivingLicenseManager;
        public DrivingLicenseController(ILogger<DrivingLicenseController> logger, IChauffeurService man, IDrivingLicenseService man2)
        {
            _logger = logger;
            _managerChaffeur = man;
            _drivingLicenseManager = man2;
        }
        // ------GET------
        [HttpGet("Drivinglicense")]
        public ActionResult<GenericResult<GeneralModels>> GetAll([FromQuery] GenericParameter parameter)
        {
            try
            {
                //_drivingLicenseManager.AddDrivingLicense(new DrivingLicense(Overall.License.AM),1);
                var temp = _drivingLicenseManager.GetAllDrivingLicensesPaging(parameter);
                var metadata = _drivingLicenseManager.GetHeaders(parameter);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(temp);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("Drivinglicense/{id}")]
        public ActionResult<GenericResult<GeneralModels>> GetFuelCardByID(int id)
        {
            try
            {
                var ch = _drivingLicenseManager.GetAllDrivingLicenseById(id);
                return (ch.StatusCode == 200) ? Ok(ch) : NotFound(ch);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
