using BusinessLayer;
using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChaffeurController : ControllerBase
    {
        private readonly ILogger<ChaffeurController> _logger;
        private IChaffeurService _managerChaffeur;
        private IVehicleService _managerVehicle;
        private IDrivingLicenseService _drivingLicenseManager;
        private IFuelCardService _fuelCardManager;
        public ChaffeurController(ILogger<ChaffeurController> logger, IChaffeurService man, IVehicleService managerVehicle, IDrivingLicenseService drivingLicenseManager, IFuelCardService fuelCardManager)
        {
            _logger = logger;
            _managerChaffeur = man;
            _managerVehicle = managerVehicle;
            _drivingLicenseManager = drivingLicenseManager;
            _fuelCardManager = fuelCardManager;
        }
        // -------POST-------
        [HttpPost]
        public ActionResult Add([FromBody] Chaffeur chaffeur)
        {
            try
            {
                var result = _managerChaffeur.AddChaffeur(chaffeur);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("{chaffeurId}/Vehicles/{vehicleId}")]
        public ActionResult<Vehicle> AddVehicleToChaffeur(int chaffeurId, int vehicleId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                var vh = _managerVehicle.GetVehicleById(vehicleId);

                var result = _managerChaffeur.AddVehicleToChaffeur(chaffeurId, vehicleId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("{chaffeurId}/Drivinglicenses")]
        public ActionResult AddDrivinglicense(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                if (ch == null)
                {
                    return NotFound("This chaffeur doesn't exist");
                }
                else
                {
                    var license = new DrivingLicense(Overall.License.B);
                    if (_drivingLicenseManager.CheckValidationDrivingLicense(license) == false)
                    {
                        return BadRequest(_drivingLicenseManager._errors);
                    }
                    else
                    {
                        if (_drivingLicenseManager.CheckExistingDrivingLicense(chaffeurId, license))
                        {
                            var result = _drivingLicenseManager.AddDrivingLicense(license, chaffeurId);
                            return Ok(result);
                        }
                        else
                        {
                            return BadRequest("Chaffeur already has this driving license.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("{chaffeurId}/FuelCards/{fuelcardId}")]
        public ActionResult AddFuelCard(int chaffeurId, int fuelcardId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                if (ch == null)
                {
                    return NotFound("This chaffeur doesn't exist.");
                }
                else
                {
                    var fc = _fuelCardManager.GetFuelCardById(fuelcardId);
                    if (fc == null)
                    {
                        return NotFound("This fuelcard doesn't exist.");
                    }
                    else
                    {
                        if (ch.CheckFuelCard(fuelcardId))
                        {
                            var result = _fuelCardManager.AddFuelCardToChaffeur(fuelcardId, chaffeurId);
                            return Ok(result);
                        }
                        else
                        {
                            return BadRequest("This fuelcard exists in chafeurs list.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // -------PUT-------
        [HttpPut("{chaffeurId}")]
        public ActionResult<Chaffeur> UpdateById(int chaffeurId)
        {
            try
            {
                DateTime date = DateTime.Now;
                var ch1 = new Chaffeur("William", "Slabbaert", "testCity", "testStreet", "132", date, "testNationalNr77", false);

                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                if (ch == null)
                {
                    return NotFound("This chaffeur doesn't exist");
                }
                else
                {
                    if (_managerChaffeur.CheckExistingChaffeur(ch1, chaffeurId))
                    {
                        if (_managerChaffeur.CheckValidationChaffeur(ch) == false)
                        {
                            return BadRequest(_managerChaffeur._errors);
                        }
                        else
                        {
                            var result = _managerChaffeur.UpdateChaffeur(ch1, chaffeurId);
                            return Ok(result);
                        }
                    }
                    else
                    {
                        return BadRequest("Chaffeur with same national insurence number already exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{chaffeurId}/Vehicles/{vehicleId}")]
        public ActionResult<Vehicle> UpdateVehicleToChaffeur(int chaffeurId, int vehicleId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                if (ch == null)
                {
                    return NotFound("This chaffeur doesn't exist");
                }
                else
                {
                    var vh = _managerVehicle.GetVehicleById(vehicleId);
                    if (vh == null)
                    {
                        return NotFound("This Vehicle doesn't exist");
                    }
                    else
                    {
                        var result = _managerChaffeur.UpdateVehicleToChaffeur(chaffeurId, vehicleId, true);
                        return Ok(result);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPut("{chaffeurId}/FuelCards/{fuelcardId}")]
        public ActionResult UpdateFuelCardActivity(int chaffeurId, int fuelcardId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                if (ch == null)
                {
                    return NotFound("This chaffeur doesn't exist.");
                }
                else
                {
                    var fc = _fuelCardManager.GetFuelCardById(fuelcardId);
                    if (fc == null)
                    {
                        return NotFound("This fuelcard doesn't exist.");
                    }
                    else
                    {
                        if (ch.CheckFuelCard(fuelcardId) == false)
                        {
                            var result = _fuelCardManager.UpdateChaffeurFuelCard(fuelcardId, chaffeurId, true);
                            return Ok(result);
                        }
                        else
                        {
                            return BadRequest("This fuelcard doesn't exist in chaffeurs list.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        // ------DELETE-------

        [HttpDelete("{chaffeurId}/Drivinglicenses/{drivinglicenseId}")]
        public ActionResult<FuelCard> DeleteDrivinglicensesByID(int chaffeurId, int drivinglicenseId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                if (ch == null)
                {
                    return NotFound("This chaffeur doesn't exist");
                }
                else
                {
                    var license = _drivingLicenseManager.GetAllDrivingLicenseById(drivinglicenseId);
                    if (license == null)
                    {
                        return NotFound("This drivinglicense doesn't exist.");
                    }
                    else
                    {
                        var temp = ch.DrivingLicenses.FirstOrDefault(s => s.Id == license.Id);
                        if (temp == null)
                        {
                            return NotFound("This drivinglicense doesn't exist in chaffeurs list.");
                        }
                        var result = _drivingLicenseManager.DeleteDrivingLicense(drivinglicenseId, chaffeurId);
                        return Ok(result);
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
