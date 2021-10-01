﻿using BusinessLayer;
using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using DataLayer.entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChaffeurController : ControllerBase
    {
        private readonly ILogger<ChaffeurController> _logger;
        private IChaffeurService _managerChaffeur;
        private IVehicleService _managerVehicle;
        private IDrivingLicenseService _drivingLicenseManager;
        public ChaffeurController(ILogger<ChaffeurController> logger, IChaffeurService man, IVehicleService managerVehicle, IDrivingLicenseService drivingLicenseManager)
        {
            _logger = logger;
            _managerChaffeur = man;
            _managerVehicle = managerVehicle;
            _drivingLicenseManager = drivingLicenseManager;
        }
        // -------GET-------

        [HttpGet]
        public ActionResult<List<Chaffeur>> GetAllChaffeurs()
        {
            try
            {
                return Ok(_managerChaffeur.GetAllChaffeurs());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Chaffeur> GetById(int id)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(id);
                if (ch == null)
                {
                    return NotFound("This chaffeur doesn't exist");
                }
                return Ok(ch);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}/Vehicles")]
        public ActionResult<List<Vehicle>> GetallVehiclesById(int id)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(id);
                if (ch == null)
                {
                    return NotFound("This chaffeur doesn't exist");
                }
                return Ok(ch.ChaffeurVehicles.Select(s => s.Vehicle));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}/Fuelcards")]
        public ActionResult<List<Vehicle>> GetallFuelCardsById(int id)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(id);
                if (ch == null)
                {
                    return NotFound("This chaffeur doesn't exist");
                }
                return Ok(ch.ChaffeurFuelCards.Select(s => s.FuelCard));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}/Requests")]
        public ActionResult<List<Vehicle>> GetallRequestsById(int id)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(id);
                if (ch == null)
                {
                    return NotFound("This chaffeur doesn't exist");
                }
                return Ok(ch.Requests);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}/Drivinglicenses")]
        public ActionResult<List<Vehicle>> GetallDrivingLicensesById(int id)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(id);
                if (ch == null)
                {
                    return NotFound("This chaffeur doesn't exist");
                }
                return Ok(ch.DrivingLicenses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // -------POST-------
        [HttpPost]
        public ActionResult Add()
        {
            try
            {
                DateTime date = DateTime.Now;
                var ch = new Chaffeur("testFirstazezae", "testLast", "testCity", "testStreet", "12", date, "testNationalNr3", true);
                if (_managerChaffeur.CheckExistingChaffeur(ch,0))
                {
                    if (_managerChaffeur.CheckValidationChaffeur(ch) == false)
                    {
                        return BadRequest(_managerChaffeur._errors);
                    }
                    else
                    {
                        var result = _managerChaffeur.AddChaffeur(ch);
                        return Ok(result);
                    }
                }
                else
                {
                    return BadRequest("Chaffeur with same national insurence number already exists.");
                }
                
            }catch(Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("{id}/Vehicles/{vehicleId}")]
        public ActionResult<Vehicle> AddVehicleToChaffeur(int id, int vehicleId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(id);
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
                        var result = _managerChaffeur.AddVehicleToChaffeur(id, vehicleId);
                        return Ok(result);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("{id}/Drivinglicenses")]
        public ActionResult AddDrivinglicense(int id)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(id);
                if (ch == null)
                {
                    return NotFound("This chaffeur doesn't exist");
                }
                else
                {
                    var license = new DrivingLicense(Overall.License.B);

                    if (_drivingLicenseManager.CheckExistingDrivingLicense(1, license))
                    {
                        var result = _drivingLicenseManager.AddDrivingLicense(license, 1);
                        if (_drivingLicenseManager._errors.Count != 0)
                        {
                            return BadRequest(_drivingLicenseManager._errors);
                        }
                        else
                        {
                            return Ok(result);
                        }
                    }
                    else
                    {
                        return BadRequest("Chaffeur already has this driving license.");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // -------PUT-------
        [HttpPut("{id}")]
        public ActionResult<Chaffeur> UpdateById(int id)
        {
            try
            {
                DateTime date = DateTime.Now;
                var ch1 = new Chaffeur("William", "Slabbaert", "testCity", "testStreet", "132", date, "testNationalNr77", false) ;

                var ch = _managerChaffeur.GetChaffeurById(id);
                if (ch == null)
                {
                    return NotFound("This chaffeur doesn't exist");
                }
                else
                {
                    if (_managerChaffeur.CheckExistingChaffeur(ch1,id))
                    {
                        if (_managerChaffeur.CheckValidationChaffeur(ch) == false)
                        {
                            return BadRequest(_managerChaffeur._errors);
                        }
                        else
                        {
                            var result = _managerChaffeur.UpdateChaffeur(ch1, id);
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
        
        [HttpPut("{id}/Vehicles/{vehicleId}")]
        public ActionResult<Vehicle> UpdateVehicleToChaffeur(int id, int vehicleId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(id);
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
                        var result = _managerChaffeur.UpdateVehicleToChaffeur(id, vehicleId,true);
                        return Ok(result);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        // ------DELETE-------
        
        [HttpDelete("{id}/Drivinglicenses/{drivinglicenseId}")]
        public ActionResult<FuelCard> DeleteFuelCardByID(int id,int drivinglicenseId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(id);
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
                        var result = _drivingLicenseManager.DeleteDrivingLicense(drivinglicenseId,id);
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
