using BusinessLayer;
using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using BusinessLayer.validators.response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private IChaffeurService _managerChaffeur;
        private IDrivingLicenseService _drivingLicenseManager;
        public DrivingLicenseController(ILogger<DrivingLicenseController> logger, IChaffeurService man, IDrivingLicenseService man2)
        {
            _logger = logger;
            _managerChaffeur = man;
            _drivingLicenseManager = man2;
        }
        // ------GET------
        [HttpGet("Drivinglicense")]
        public ActionResult<GenericResult> GetAll()
        {
            try
            {
                //_drivingLicenseManager.AddDrivingLicense(new DrivingLicense(Overall.License.AM),1);
                return Ok(_drivingLicenseManager.GetAllDrivingLicenses());
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("Drivinglicense/{id}")]
        public ActionResult<FuelCard> GetFuelCardByID(int id)
        {
            try
            {
                var vh = _drivingLicenseManager.GetAllDrivingLicenseById(id);
                return Ok(vh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        
        [HttpGet("Drivinglicense/{id}/Chaffeurs")]
        public ActionResult<Chaffeur> GetFuelCardByIDChaffeur(int id)
        {
            try
            {
                var vh = _drivingLicenseManager.GetDrivingLicenseChaffeurById(id);
                return Ok(vh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
