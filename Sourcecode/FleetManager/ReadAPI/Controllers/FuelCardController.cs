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
        // ------GET-------
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
                return Ok(vh.ChaffeurFuelCards.Select(s => s.Chaffeur));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Fuelcard/{id}/Fueltypes")]
        public ActionResult<List<FuelType>> GetFuelCardFuelsByID(int id)
        {
            try
            {
                var vh = _fuelCardManager.GetFuelCardById(id);
                if (vh == null)
                {
                    return NotFound("This fuelcard doesn't exist");
                }
                return Ok(vh.FuelType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Fuelcard/{id}/Authentications")]
        public ActionResult<List<FuelType>> GetFuelCardAuthenticationTypesByID(int id)
        {
            try
            {
                var vh = _fuelCardManager.GetFuelCardById(id);
                if (vh == null)
                {
                    return NotFound("This fuelcard doesn't exist");
                }
                return Ok(vh.AuthenticationTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
  
        // ------POST------
        [HttpPost("Fuelcard")]
        public ActionResult Add()
        {
            var temp = new FuelCard("123", "1234", true,DateTime.Now);
            if (_fuelCardManager.CheckValidationFuelCard(temp) == false)
            {
                return BadRequest(_fuelCardManager._errors);
            }
            else
            {
                if (_fuelCardManager.CheckExistingFuelCard(temp))
                {
                    var result = _fuelCardManager.AddFuelCard(temp);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Fuelcard already exists.");
                }
            }
        }
        
        [HttpPost("Fuelcard/{id}/Fueltypes")]
        public ActionResult<FuelCard> AddFuelsTpFuelcardByID(int id)
        {
            try
            {
                var fueltype = new FuelType(Overall.FuelTypes.Diesel);
                var vh = _fuelCardManager.GetFuelCardById(id);
                if (vh == null)
                {
                    return NotFound("This fuelcard doesn't exist");
                }
                else
                {
                    
                    if (_fuelCardManager.CheckValidationFuelType(fueltype) == false)
                    {
                        return BadRequest(_fuelCardManager._errors);
                    }
                    else
                    {
                        var check = vh.CheckExistingFuelType(fueltype);
                        if (check)
                        {
                            var fuelcard = _fuelCardManager.AddFuelType(id, fueltype);
                            return Ok(fuelcard);
                        }
                        else
                        {
                            return BadRequest("This fueltype already exists in fuelcards list.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("Fuelcard/{id}/Services")]
        public ActionResult<FuelCard> AddService(int id)
        {
            try
            {
                var vh = _fuelCardManager.GetFuelCardById(id);
                var ser = new ExtraService(Overall.ExtraServices.DiscountCarwash);
                if (vh == null)
                {
                    return NotFound("This fuelcard doesn't exist");
                }
                else
                {
                    if (_fuelCardManager.CheckValidationService(ser))
                    {
                        if (vh.CheckExistingSerives(ser))
                        {
                            var temp = _fuelCardManager.AddService(ser, id);
                            return Ok(temp);
                        }
                        else
                        {
                            return BadRequest("Service already exists in fuelcards list.");
                        }
                    }
                    else
                    {
                        return BadRequest(_fuelCardManager._errors);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("Fuelcard/{id}/Authentications")]
        public ActionResult<FuelCard> AddFuelCardAuthenticationTypesByID(int id)
        {
            try
            {
                var vh = _fuelCardManager.GetFuelCardById(id);
                var temp = new AuthenticationType(Overall.AuthenticationTypes.PINKM);
                if (vh == null)
                {
                    return NotFound("This fuelcard doesn't exist");
                }
                else
                {
                    if(_fuelCardManager.CheckValidationAuthentication(temp) == false)
                    {
                        return BadRequest(_fuelCardManager._errors);
                    }
                    else
                    {
                        if (vh.CheckExistingAuthentications(temp))
                        {
                            var result = _fuelCardManager.AddAuthentication(temp,id);
                            return Ok(result);
                        }
                        else
                        {
                            return BadRequest("This fuelcard already has this authentication type.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // -------PUT-------

        [HttpPut("Fuelcard/{id}")]
        public ActionResult<FuelCard> UpdateFuelCardFuelsByID(int id)
        {
            try
            {
                var vh = _fuelCardManager.GetFuelCardById(id);
                var temp = new FuelCard("testPut2", "4321", true, DateTime.Now) { Id=id};
                if (vh == null)
                {
                    return NotFound("This fuelcard doesn't exist");
                }
                else
                {
                    if (_fuelCardManager.CheckExistingFuelCard(temp))
                    {
                        var result = _fuelCardManager.UpdateFuelCard(temp, id);
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("Fuelcard with same cardnumber already exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        // -------DELETE-------

        [HttpDelete("Fuelcard/{id}/Fueltypes/{fuelId}")]
        public ActionResult<FuelCard> DeleteFuelFromFuelcardByID(int id,int fuelId)
        {
            try
            {
                var vh = _fuelCardManager.GetFuelCardById(id);
                if (vh == null)
                {
                    return NotFound("This fuelcard doesn't exist");
                }
                else
                {
                    var fl = vh.FuelType.FirstOrDefault(s => s.Id == fuelId);
                    if(fl == null)
                    {
                        return NotFound("This fueltype doesn't exist in this fuelcards list.");
                    }
                    else
                    {
                        var temp = _fuelCardManager.DeleteFuelType(id,fuelId);
                        return Ok(temp);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpDelete("Fuelcard/{id}/Services/{service}")]
        public ActionResult<FuelCard> DeleteService(int id,int service)
        {
            try
            {
                var vh = _fuelCardManager.GetFuelCardById(id);
                if (vh == null)
                {
                    return NotFound("This fuelcard doesn't exist");
                }
                else
                {
                    var temp = vh.Services.FirstOrDefault(s => s.Id == service);
                    if (temp != null)
                    {
                        var result = _fuelCardManager.DeleteService(service,id);
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("Service doesn't exist in fuelcards list.");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
