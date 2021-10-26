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
    public class ChauffeurController : ControllerBase
    {
        private readonly ILogger<ChauffeurController> _logger;
        private IChauffeurService _managerChaffeur;
        private IDrivingLicenseService _drivingLicenseManager;
        private IMediator _mediator;
        private IFuelCardService _fuelCardManager;
        private IRequestService _requestService;
        public ChauffeurController(ILogger<ChauffeurController> logger, IChauffeurService man, IDrivingLicenseService drivingLicenseManager, IFuelCardService fuelCardManager, IMediator mediator, IRequestService requestService)
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
        [HttpPut("{chauffeurId}")]
        public ActionResult<GenericResult<GeneralModels>> UpdateById(int chauffeurId, [FromBody] ChauffeurDTO chaffeur)
        {
            try
            {
                var check = _managerChaffeur.GetChauffeurById(chauffeurId);
                if (check.StatusCode == 404)
                {
                    return NotFound(check);
                }

                var result = _managerChaffeur.UpdateChauffeur(chaffeur, chauffeurId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("{chauffeurId}/Vehicle/{vehicleId}")]
        public ActionResult<GenericResult<GeneralModels>> AddVehicleToChauffeur(int chauffeurId, int vehicleId)
        {
            try
            {
                var ch = _managerChaffeur.GetChauffeurById(chauffeurId);
                var vh = _mediator.Send(new GetVehicleByIdQuery(vehicleId)).Result;
                if (ch.StatusCode == 404 || vh.StatusCode == 404)
                {
                    return ch.StatusCode == 404 ? NotFound(ch) : NotFound(vh);
                }

                var result = _managerChaffeur.AddVehicleToChauffeur(chauffeurId, vehicleId);
                return result.StatusCode != 200 ? BadRequest(result) : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPatch("{chauffeurId}/Vehicle/{vehicleId}")]
        public ActionResult<GenericResult<GeneralModels>> UpdateVehicleToChauffeur(int chauffeurId, int vehicleId, [FromBody] bool activity)
        {
            try
            {
                var ch = _managerChaffeur.GetChauffeurById(chauffeurId);
                var vh = _mediator.Send(new GetVehicleByIdQuery(vehicleId)).Result;
                if (ch.StatusCode == 404 || vh.StatusCode == 404)
                {
                    return ch.StatusCode == 404 ? NotFound(ch) : NotFound(vh);
                }
                var result = _managerChaffeur.UpdateVehicleToChauffeur(chauffeurId, vehicleId, activity);
                return result.StatusCode != 200 ? BadRequest(result) : Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPost("{chauffeurId}/Drivinglicense")]
        public ActionResult<GenericResult<GeneralModels>> AddDrivinglicense(int chauffeurId, [FromBody] DrivingLicenseDTO drivingLicense)
        {
            try
            {
                var check = _managerChaffeur.GetChauffeurById(chauffeurId);
                if (check.StatusCode == 404)
                {
                    return NotFound(check);
                }
                var result = _drivingLicenseManager.AddDrivingLicense(drivingLicense,chauffeurId);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{chauffeurId}/Drivinglicense/{drivinglicenseId}")]
        public ActionResult<GenericResult<GeneralModels>> DeleteDrivinglicensesByID(int chauffeurId, int drivinglicenseId)
        {
            try
            {
                var check = _managerChaffeur.GetChauffeurById(chauffeurId);
                var check2 = _drivingLicenseManager.GetAllDrivingLicenseById(drivinglicenseId);
                if (check.StatusCode != 200 || check2.StatusCode != 200)
                {
                    return check.StatusCode != 200 ? NotFound(check) : NotFound(check2);
                }
                var result = _drivingLicenseManager.DeleteDrivingLicense(drivinglicenseId,chauffeurId);
                return result.StatusCode == 200 ? Ok(result) : NotFound(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("{chauffeurId}/FuelCard/{fuelcardId}")]
        public ActionResult<GenericResult<GeneralModels>> AddFuelCard(int chauffeurId, int fuelcardId)
        {
            try
            {
                var check = _managerChaffeur.GetChauffeurById(chauffeurId);
                var check2 = _fuelCardManager.GetFuelCardById(fuelcardId);
                if (check.StatusCode != 200 || check2.StatusCode != 200)
                {
                    return check.StatusCode != 200 ? NotFound(check) : NotFound(check2);
                }
                var result = _fuelCardManager.AddFuelCardToChauffeur(fuelcardId,chauffeurId);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPatch("{chauffeurId}/FuelCard/{fuelcardId}")]
        public ActionResult<GenericResult<GeneralModels>> UpdateFuelCardActivity(int chauffeurId, int fuelcardId, [FromBody] bool activity)
        {
            try
            {
                var check = _managerChaffeur.GetChauffeurById(chauffeurId);
                var check2 = _fuelCardManager.GetFuelCardById(fuelcardId);
                if (check.StatusCode != 200 || check2.StatusCode != 200)
                {
                    return check.StatusCode != 200 ? NotFound(check) : NotFound(check2);
                }
                var result = _fuelCardManager.ActivityChauffeurFuelCard(fuelcardId, chauffeurId, activity);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("{chauffeurId}/Vehicle/{vehicleId}/Requests")]
        public ActionResult<GenericResult<GeneralModels>> AddRequest(int chauffeurId, int vehicleId, [FromBody] RequestDTO request)
        {
            try
            {
                var check = _managerChaffeur.GetChauffeurById(chauffeurId);
                var check2 = _mediator.Send(new GetVehicleByIdFromChauffeurQuery(chauffeurId,vehicleId)).Result;
                if (check.StatusCode != 200 || check2.StatusCode != 200)
                {
                    return check.StatusCode != 200 ? NotFound(check) : NotFound(check2);
                }
                var result = _requestService.AddRequest(request, chauffeurId, vehicleId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
