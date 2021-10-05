using BusinessLayer;
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
        private IFuelCardService _fuelCardManager;
        public ChaffeurController(ILogger<ChaffeurController> logger, IChaffeurService man, IFuelCardService fuelCardManager)
        {
            this._logger = logger;
            this._managerChaffeur = man;
            this._fuelCardManager = fuelCardManager;
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
        [HttpGet("{chaffeurId}")]
        public ActionResult<Chaffeur> GetById(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                return Ok(ch);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{chaffeurId}/Vehicles")]
        public ActionResult<List<Vehicle>> GetallVehiclesById(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                return Ok(ch.ChaffeurVehicles.Select(s => s.Vehicle));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{chaffeurId}/Fuelcards")]
        public ActionResult<List<FuelCard>> GetallFuelCardsById(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                return Ok(ch.ChaffeurFuelCards.Select(s => s.FuelCard));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{chaffeurId}/Fuelcards/{fuelcardId}")]
        public ActionResult<FuelCard> GetFuelCard(int chaffeurId, int fuelcardId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                var fc = _fuelCardManager.GetFuelCardById(fuelcardId);
                var result = _managerChaffeur.GetFuelcardFromChaffeur(ch,fuelcardId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("{chaffeurId}/Requests")]
        public ActionResult<List<Vehicle>> GetallRequestsById(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                return Ok(ch.Requests);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{chaffeurId}/Drivinglicenses")]
        public ActionResult<List<Vehicle>> GetallDrivingLicensesById(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                return Ok(ch.DrivingLicenses);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
