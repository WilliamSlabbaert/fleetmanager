using BusinessLayer;
using BusinessLayer.services.interfaces;
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
    public class ChauffeurController : ControllerBase
    {
        private IChauffeurService _managerChaffeur;
        public ChauffeurController( IChauffeurService man)
        {
            this._managerChaffeur = man;
        }
        // -------GET-------

        [HttpGet]
        public ActionResult<GenericResult<GeneralModels>> GetAllChaffeurs([FromQuery] GenericParameter parameter)
        {

            var temp = _managerChaffeur.GetAllChauffeursPaging(parameter);
            //var metadata = _managerChaffeur.GetHeaders(parameter);
            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
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
        public string Test()
        {
            return "et";
        }
    }
}
