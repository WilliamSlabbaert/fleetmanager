using BusinessLayer;
using BusinessLayer.managers.interfaces;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChaffeurController : ControllerBase
    {
        private readonly ILogger<ChaffeurController> _logger;
        private IChaffeurService _managerChaffeur;
        private IDrivingLicenseService _drivingLicenseManager;
        private IMediator _mediator;
        private IFuelCardService _fuelCardManager;
        public ChaffeurController(ILogger<ChaffeurController> logger, IChaffeurService man, IDrivingLicenseService drivingLicenseManager, IFuelCardService fuelCardManager, IMediator mediator)
        {
            _logger = logger;
            _managerChaffeur = man;
            _drivingLicenseManager = drivingLicenseManager;
            _fuelCardManager = fuelCardManager;
            _mediator = mediator;
        }
        // -------POST-------
        [HttpPost]
        public ActionResult<GenericResult<IGeneralModels>> Add([FromBody] Chaffeur chaffeur)
        {
            try
            {
                var result = _managerChaffeur.AddChaffeur(chaffeur);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{chaffeurId}")]
        public ActionResult<GenericResult<IGeneralModels>> UpdateById(int chaffeurId, [FromBody] Chaffeur chaffeur)
        {
            try
            {
                var check = _managerChaffeur.GetChaffeurById(chaffeurId);
                if (check.StatusCode == 404)
                {
                    return NotFound(check);
                }

                chaffeur.Id = chaffeurId;
                var result = _managerChaffeur.UpdateChaffeur(chaffeur, chaffeurId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("{chaffeurId}/Vehicles/{vehicleId}")]
        public ActionResult<GenericResult<IGeneralModels>> AddVehicleToChaffeur(int chaffeurId, int vehicleId)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                var vh = _mediator.Send(new GetVehicleByIdQuery(vehicleId)).Result;
                if (ch.StatusCode == 404 || vh.StatusCode == 404)
                {
                    return ch.StatusCode == 404 ? NotFound(ch) : NotFound(vh);
                }

                var result = _managerChaffeur.AddVehicleToChaffeur(chaffeurId, vehicleId);
                return result.StatusCode != 200 ? BadRequest(result) : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPatch("{chaffeurId}/Vehicles/{vehicleId}")]
        public ActionResult<GenericResult<IGeneralModels>> UpdateVehicleToChaffeur(int chaffeurId, int vehicleId, [FromBody] bool activity)
        {
            try
            {
                var ch = _managerChaffeur.GetChaffeurById(chaffeurId);
                var vh = _mediator.Send(new GetVehicleByIdQuery(vehicleId)).Result;
                if (ch.StatusCode == 404 || vh.StatusCode == 404)
                {
                    return ch.StatusCode == 404 ? NotFound(ch) : NotFound(vh);
                }
                var result = _managerChaffeur.UpdateVehicleToChaffeur(chaffeurId, vehicleId, activity);
                return result.StatusCode != 200 ? BadRequest(result) : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPost("{chaffeurId}/Drivinglicenses")]
        public ActionResult<GenericResult<IGeneralModels>> AddDrivinglicense(int chaffeurId, [FromBody] DrivingLicense drivingLicense)
        {
            try
            {
                var check = _managerChaffeur.GetChaffeurById(chaffeurId);
                if (check.StatusCode == 404)
                {
                    return NotFound(check);
                }
                var result = _drivingLicenseManager.AddDrivingLicense(drivingLicense,chaffeurId);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{chaffeurId}/Drivinglicenses/{drivinglicenseId}")]
        public ActionResult<FuelCard> DeleteDrivinglicensesByID(int chaffeurId, int drivinglicenseId)
        {
            try
            {
                var check = _managerChaffeur.GetChaffeurById(chaffeurId);
                var check2 = _drivingLicenseManager.GetAllDrivingLicenseById(drivinglicenseId);
                if (check.StatusCode != 200 || check2.StatusCode != 200)
                {
                    return check.StatusCode != 200 ? NotFound(check) : NotFound(check2);
                }
                var result = _drivingLicenseManager.DeleteDrivingLicense(drivinglicenseId,chaffeurId);
                return result.StatusCode == 200 ? Ok(result) : NotFound(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("{chaffeurId}/FuelCards/{fuelcardId}")]
        public ActionResult AddFuelCard(int chaffeurId, int fuelcardId)
        {
            try
            {
                var check = _managerChaffeur.GetChaffeurById(chaffeurId);
                var check2 = _fuelCardManager.GetFuelCardById(fuelcardId);
                if (check.StatusCode != 200 || check2.StatusCode != 200)
                {
                    return check.StatusCode != 200 ? NotFound(check) : NotFound(check2);
                }
                var result = _fuelCardManager.AddFuelCardToChaffeur(fuelcardId,chaffeurId);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPatch("{chaffeurId}/FuelCards/{fuelcardId}")]
        public ActionResult UpdateFuelCardActivity(int chaffeurId, int fuelcardId, [FromBody] bool activity)
        {
            try
            {
                var check = _managerChaffeur.GetChaffeurById(chaffeurId);
                var check2 = _fuelCardManager.GetFuelCardById(fuelcardId);
                if (check.StatusCode != 200 || check2.StatusCode != 200)
                {
                    return check.StatusCode != 200 ? NotFound(check) : NotFound(check2);
                }
                var result = _fuelCardManager.UpdateChaffeurFuelCard(fuelcardId, chaffeurId,activity);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
