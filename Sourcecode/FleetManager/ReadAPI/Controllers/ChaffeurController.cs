using BusinessLayer;
using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using BusinessLayer.validators.response;
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
        public ActionResult<GenericResult> GetAllChaffeurs()
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
        public ActionResult<GenericResult> GetById(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                
                return Ok(ch);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{chaffeurId}/Vehicles")]
        public ActionResult<GenericResult> GetallVehiclesById(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurVehicles(chaffeurId);
                return Ok(ch);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{chaffeurId}/Fuelcards")]
        public ActionResult<GenericResult> GetallFuelCards(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurFuelcards(chaffeurId);
                return Ok(ch);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{chaffeurId}/Requests")]
        public ActionResult<GenericResult> GetallRequests(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurRequests(chaffeurId);
                return Ok(ch);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{chaffeurId}/Drivinglicenses")]
        public ActionResult<GenericResult> GetallDrivingLicensesById(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurDrivingLicenses(chaffeurId);
                return Ok(ch);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
