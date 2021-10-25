using BusinessLayer;
using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using DataLayer.entities;
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
    [Route("[controller]")]
    public class ChaffeurController : ControllerBase
    {
        private readonly ILogger<ChaffeurController> _logger;
        private IChauffeurService _managerChaffeur;
        private IFuelCardService _fuelCardManager;
        public ChaffeurController(ILogger<ChaffeurController> logger, IChauffeurService man, IFuelCardService fuelCardManager)
        {
            this._logger = logger;
            this._managerChaffeur = man;
            this._fuelCardManager = fuelCardManager;
        }
        // -------GET-------

        [HttpGet]
        public ActionResult<GenericResult<GeneralModels>> GetAllChaffeurs([FromQuery] GenericParameter parameter)
        {

            var temp = _managerChaffeur.GetAllChauffeursPaging(parameter);
            var metadata = _managerChaffeur.GetHeaders(parameter);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return (temp.StatusCode == 200) ? Ok(temp) : BadRequest(temp);


        }
        [HttpGet("{chaffeurId}")]
        public ActionResult<GenericResult<GeneralModels>> GetById(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChauffeurById(chaffeurId);

                return (ch.StatusCode == 200) ? Ok(ch) : NotFound(ch);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{chaffeurId}/Vehicle")]
        public ActionResult<GenericResult<GeneralModels>> GetallVehiclesById(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChauffeurVehicles(chaffeurId);
                return (ch.StatusCode == 200) ? Ok(ch) : NotFound(ch);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{chaffeurId}/Fuelcard")]
        public ActionResult<GenericResult<GeneralModels>> GetallFuelCards(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChauffeurFuelcards(chaffeurId);
                return (ch.StatusCode == 200) ? Ok(ch) : NotFound(ch);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{chaffeurId}/Request")]
        public ActionResult<GenericResult<GeneralModels>> GetallRequests(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChauffeurRequests(chaffeurId);
                return (ch.StatusCode == 200) ? Ok(ch) : NotFound(ch);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{chaffeurId}/Drivinglicense")]
        public ActionResult<GenericResult<GeneralModels>> GetallDrivingLicensesById(int chaffeurId)
        {
            try
            {
                var ch = _managerChaffeur.GetChauffeurDrivingLicenses(chaffeurId);
                return (ch.StatusCode == 200) ? Ok(ch) : NotFound(ch);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
