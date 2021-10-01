using BusinessLayer;
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
        [HttpGet("Drivinglicense")]
        public ActionResult<List<DrivingLicense>> GetAll()
        {
            try
            {
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
                if (vh == null)
                {
                    return NotFound("This drivinglicense doesn't exist");
                }
                return Ok(vh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        
        [HttpGet("Drivinglicense/{id}/Chaffeur")]
        public ActionResult<Chaffeur> GetFuelCardByIDChaffeur(int id)
        {
            try
            {
                var vh = _drivingLicenseManager.GetAllDrivingLicenseById(id);
                if (vh == null)
                {
                    return NotFound("This drivinglicense doesn't exist");
                }
                return Ok(vh.Chaffeur);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
