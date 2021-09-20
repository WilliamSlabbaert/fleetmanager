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
    public class VehicleController : Controller
    {
        private readonly ILogger<ChaffeurController> _logger;
        private IChaffeurManager _managerChaffeur;
        private IVehicleManager _managerVehicle;
        public VehicleController(ILogger<ChaffeurController> logger, IChaffeurManager man, IVehicleManager man2)
        {
            _logger = logger;
            _managerChaffeur = man;
            _managerVehicle = man2;
        }
        [HttpGet("Vehicle")]
        public ActionResult<Vehicle> GetAllVehicles()
        {
            try
            {
                return Ok(_managerVehicle.GetAllVehicles());
            }catch(Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("Vehicle/{id}")]
        public ActionResult<Vehicle> GetVehicleByID(int id)
        {
            try
            {
                var vh = _managerVehicle.GetVehicleById(id);
                if (vh == null)
                {
                    return NotFound("This vehicle doesn't exist");
                }
                return Ok(vh);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Vehicle/{id}/Chaffeurs")]
        public ActionResult<List<Chaffeur>> GetVehicleChaffeursByID(int id)
        {
            try
            {
                var vh = _managerVehicle.GetVehicleById(id);
                if (vh == null)
                {
                    return NotFound("This vehicle doesn't exist");
                }
                return Ok(vh.ChaffeurVehicles.Select(s=>s.Chaffeur));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
