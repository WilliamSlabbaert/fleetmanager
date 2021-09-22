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
        private IVehicleService _managerVehicle;
        public ChaffeurController(ILogger<ChaffeurController> logger,IChaffeurService man, IVehicleService man2)
        {
            _logger = logger;
            _managerChaffeur = man;
            _managerVehicle = man2;
        }

        [HttpGet]
        public ActionResult<List<Chaffeur>> GetAllChaffeurs()
        {
            try
            {
                DateTime date = DateTime.Now;
                //date = date.AddDays(1);
                var temp = _managerChaffeur.test(new Chaffeur("testFirst", "testLast", "testCity", "testStreet", "12", date, "testNationalNr", true));
                //_managerChaffeur.AddChaffeur(new Chaffeur("testFirst","testLast","testCity","testStreet","testNr",DateTime.Now,"testNationalNr",true));
                //_managerVehicle.AddVehicle(new Vehicle(123,Overall.CarTypes.Passengercar,111,Overall.FuelTypes.Diesel));

                //_managerChaffeur.RemoveVehicleToChaffeur(1,1);
                //_managerChaffeur.AddVehicleToChaffeur(1, 1);

                //var temp = _managerChaffeur.GetAllChaffeurs();
                return Ok(temp);
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
                if(ch == null)
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
                return Ok(ch.ChaffeurVehicles.Select(s=>s.Vehicle));
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
