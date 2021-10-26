using BusinessLayer;
using BusinessLayer.services.interfaces;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.models.input;
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
        private IChauffeurService _managerChaffeur;
        private IDrivingLicenseService _drivingLicenseManager;
        private IMediator _mediator;
        private IFuelCardService _fuelCardManager;
        private IRequestService _requestService;
        public ChaffeurController(ILogger<ChaffeurController> logger, IChauffeurService man, IDrivingLicenseService drivingLicenseManager, IFuelCardService fuelCardManager, IMediator mediator, IRequestService requestService)
        {
            _logger = logger;
            _managerChaffeur = man;
            _drivingLicenseManager = drivingLicenseManager;
            _fuelCardManager = fuelCardManager;
            _mediator = mediator;
            _requestService = requestService;
        }
        [HttpPost]
        public ActionResult<GenericResult<GeneralModels>> Add([FromBody] ChauffeurDTO chaffeur)
        {
            try
            {
                var result = _managerChaffeur.AddChauffeur(chaffeur);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{chaffeurId}")]
        public ActionResult<GenericResult<GeneralModels>> UpdateById(int chaffeurId, [FromBody] ChauffeurDTO chaffeur)
        {
            try
            {
                var check = _managerChaffeur.GetChauffeurById(chaffeurId);
                if (check.StatusCode == 404)
                {
                    return NotFound(check);
                }

                var result = _managerChaffeur.UpdateChauffeur(chaffeur, chaffeurId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("{chaffeurId}/Vehicle/{vehicleId}")]
        public ActionResult<GenericResult<GeneralModels>> AddVehicleToChaffeur(int chaffeurId, int vehicleId)
        {
            try
            {
                var ch = _managerChaffeur.GetChauffeurById(chaffeurId);
                var vh = _mediator.Send(new GetVehicleByIdQuery(vehicleId)).Result;
                if (ch.StatusCode == 404 || vh.StatusCode == 404)
                {
                    return ch.StatusCode == 404 ? NotFound(ch) : NotFound(vh);
                }

                var result = _managerChaffeur.AddVehicleToChauffeur(chaffeurId, vehicleId);
                return result.StatusCode != 200 ? BadRequest(result) : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPatch("{chaffeurId}/Vehicle/{vehicleId}")]
        public ActionResult<GenericResult<GeneralModels>> UpdateVehicleToChaffeur(int chaffeurId, int vehicleId, [FromBody] bool activity)
        {
            try
            {
                var ch = _managerChaffeur.GetChauffeurById(chaffeurId);
                var vh = _mediator.Send(new GetVehicleByIdQuery(vehicleId)).Result;
                if (ch.StatusCode == 404 || vh.StatusCode == 404)
                {
                    return ch.StatusCode == 404 ? NotFound(ch) : NotFound(vh);
                }
                var result = _managerChaffeur.UpdateVehicleToChauffeur(chaffeurId, vehicleId, activity);
                return result.StatusCode != 200 ? BadRequest(result) : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPost("{chaffeurId}/Drivinglicense")]
        public ActionResult<GenericResult<GeneralModels>> AddDrivinglicense(int chaffeurId, [FromBody] DrivingLicenseDTO drivingLicense)
        {
            try
            {
                var check = _managerChaffeur.GetChauffeurById(chaffeurId);
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

        [HttpDelete("{chaffeurId}/Drivinglicense/{drivinglicenseId}")]
        public ActionResult<GenericResult<GeneralModels>> DeleteDrivinglicensesByID(int chaffeurId, int drivinglicenseId)
        {
            try
            {
                var check = _managerChaffeur.GetChauffeurById(chaffeurId);
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

        [HttpPost("{chaffeurId}/FuelCard/{fuelcardId}")]
        public ActionResult<GenericResult<GeneralModels>> AddFuelCard(int chaffeurId, int fuelcardId)
        {
            try
            {
                var check = _managerChaffeur.GetChauffeurById(chaffeurId);
                var check2 = _fuelCardManager.GetFuelCardById(fuelcardId);
                if (check.StatusCode != 200 || check2.StatusCode != 200)
                {
                    return check.StatusCode != 200 ? NotFound(check) : NotFound(check2);
                }
                var result = _fuelCardManager.AddFuelCardToChauffeur(fuelcardId,chaffeurId);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPatch("{chaffeurId}/FuelCard/{fuelcardId}")]
        public ActionResult<GenericResult<GeneralModels>> UpdateFuelCardActivity(int chaffeurId, int fuelcardId, [FromBody] bool activity)
        {
            try
            {
                var check = _managerChaffeur.GetChauffeurById(chaffeurId);
                var check2 = _fuelCardManager.GetFuelCardById(fuelcardId);
                if (check.StatusCode != 200 || check2.StatusCode != 200)
                {
                    return check.StatusCode != 200 ? NotFound(check) : NotFound(check2);
                }
                var result = _fuelCardManager.ActivityChauffeurFuelCard(fuelcardId, chaffeurId, activity);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("{chaffeurId}/Vehicle/{vehicleId}/Requests")]
        public ActionResult<GenericResult<GeneralModels>> AddRequest(int chaffeurId, int vehicleId, [FromBody] RequestDTO request)
        {
            try
            {
                var check = _managerChaffeur.GetChauffeurById(chaffeurId);
                var check2 = _mediator.Send(new GetVehicleByIdFromChauffeurQuery(chaffeurId,vehicleId)).Result;
                if (check.StatusCode != 200 || check2.StatusCode != 200)
                {
                    return check.StatusCode != 200 ? NotFound(check) : NotFound(check2);
                }
                var result = _requestService.AddRequest(request, chaffeurId, vehicleId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
