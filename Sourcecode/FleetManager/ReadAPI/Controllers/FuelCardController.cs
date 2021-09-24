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
    public class FuelCardController : Controller
    {
        private readonly ILogger<FuelCardController> _logger;
        private IChaffeurService _managerChaffeur;
        private IFuelCardService _fuelCardManager ;
        public FuelCardController(ILogger<FuelCardController> logger, IChaffeurService man, IFuelCardService man2)
        {
            _logger = logger;
            _managerChaffeur = man;
            _fuelCardManager = man2;
        }

        [HttpGet("Fuelcard")]
        public ActionResult<List<FuelCard>> Get()
        {
            try
            {
                var temp = _fuelCardManager.GetAllFuelCards();
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("Fuelcard")]
        public ActionResult Add()
        {
            //_fuelCardManager.AddFuelCard(new FuelCard("testNr","1234",true));
            _fuelCardManager.AddFuelCardToChaffeur(1, 1);
            if(_fuelCardManager._errors.Count != 0)
            {
                return BadRequest(_fuelCardManager._errors);
            }
            else
            {
                return Ok(_fuelCardManager.GetAllFuelCards());
            }

        }
        [HttpGet("Fuelcard/{id}")]
        public ActionResult<FuelCard> GetFuelCardByID(int id)
        {
            try
            {
                var vh = _fuelCardManager.GetFuelCardById(id);
                if (vh == null)
                {
                    return NotFound("This fuelcard doesn't exist");
                }
                return Ok(vh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Fuelcard/{id}/Chaffeurs")]
        public ActionResult<List<Chaffeur>> GetFuelCardChaffeursByID(int id)
        {
            try
            {
                var vh = _fuelCardManager.GetFuelCardById(id);
                if (vh == null)
                {
                    return NotFound("This fuelcard doesn't exist");
                }
                return Ok(vh.ChaffeurFuelCards.Select(s=>s.Chaffeur));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
