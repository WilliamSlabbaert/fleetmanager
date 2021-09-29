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
        public ChaffeurController(ILogger<ChaffeurController> logger, IChaffeurService man)
        {
            _logger = logger;
            _managerChaffeur = man;
        }

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
        [HttpPost]
        public ActionResult Add()
        {
            try
            {
                DateTime date = DateTime.Now;
                var ch = new Chaffeur("testFirst", "testLast", "testCity", "testStreet", "12", date, "testNationalNr2", true);
                if (_managerChaffeur.checkExistingChaffeur(ch))
                {
                    var result = _managerChaffeur.AddChaffeur(ch);
                    if (_managerChaffeur._errors.Count != 0)
                    {
                        return BadRequest(_managerChaffeur._errors);
                    }
                    else
                    {
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

    }
}
