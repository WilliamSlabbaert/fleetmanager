﻿using BusinessLayer;
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
        private IChaffeurManager _managerChaffeur;
        private IVehicleManager _managerVehicle;
        public ChaffeurController(ILogger<ChaffeurController> logger,IChaffeurManager man, IVehicleManager man2)
        {
            _logger = logger;
            _managerChaffeur = man;
            _managerVehicle = man2;
        }

        [HttpGet]
        public ActionResult<List<Chaffeur>> Get()
        {
            try
            {
                var vh = _managerVehicle.GetVehicleById(1);
                var ch = _managerChaffeur.GetChaffeurById(1);

                var temp = _managerChaffeur.GetAllChaffeursWithoutIncludes();

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
                    return NotFound("This car doesn't exist");
                }
                return Ok(ch);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
