using BusinessLayer;
using BusinessLayer.services.interfaces;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadAPI.Controllers
{
    [ApiController]
    public class FuelCardController : Controller
    {
        
        private readonly ILogger<FuelCardController> _logger;
        private IChauffeurService _managerChaffeur;
        private IFuelCardService _fuelCardManager ;
        public FuelCardController(ILogger<FuelCardController> logger, IChauffeurService man, IFuelCardService man2)
        {
            _logger = logger;
            _managerChaffeur = man;
            _fuelCardManager = man2;
        }
        // ------GET-------
        [HttpGet("Fuelcard")]
        public ActionResult<GenericResult<GeneralModels>> Get([FromQuery] GenericParameter parameter)
        {
            try
            {
                var temp = _fuelCardManager.GetAllFuelCardsPaging(parameter);
                var metadata = _fuelCardManager.GetHeaders(parameter);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Fuelcard/{id}")]
        public ActionResult<GenericResult<GeneralModels>> GetFuelCardByID(int id)
        {
            try
            {
                var temp = _fuelCardManager.GetFuelCardById(id);
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Fuelcard/{id}/Chaffeur")]
        public ActionResult<GenericResult<GeneralModels>> GetFuelCardChaffeursByID(int id)
        {
            try
            {
                var temp = _fuelCardManager.GetFuelcardChauffeurs(id);
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Fuelcard/{id}/Fueltype")]
        public ActionResult<GenericResult<GeneralModels>> GetFuelCardFuelsByID(int id)
        {
            try
            {
                var temp = _fuelCardManager.GetFuelcardFuelTypes((id));
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Fuelcard/{id}/Authentication")]
        public ActionResult<GenericResult<GeneralModels>> GetFuelCardAuthenticationTypesByID(int id)
        {
            try
            {
                var temp = _fuelCardManager.GetFuelcardAuthenications(id);
                return (temp.StatusCode == 200) ? Ok(temp) : NotFound(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
