using BusinessLayer.services.interfaces;
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
    public class FuelCardController : Controller
    {
        private readonly ILogger<FuelCardController> _logger;
        private IFuelCardService _fuelCardManager;
        public FuelCardController(ILogger<FuelCardController> logger, IFuelCardService fuelCardManager)
        {
            _logger = logger;
            _fuelCardManager = fuelCardManager;
        }
        [HttpPost("FuelCard")]
        public ActionResult<GenericResult<GeneralModels>> AddFuelCard([FromBody] FuelCardDTO fuelcard)
        {
            try
            {
                var result = _fuelCardManager.AddFuelCard(fuelcard);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("FuelCard/{fuelcardId}")]
        public ActionResult<GenericResult<GeneralModels>> updateFuelCard(int fuelcardId,[FromBody] FuelCardDTO fuelCard)
        {
            try
            {
                var check = _fuelCardManager.GetFuelCardById(fuelcardId);
                if (check.StatusCode != 200)
                {
                    return NotFound(check);
                }
                var result = _fuelCardManager.UpdateFuelCard(fuelcardId, fuelCard);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("FuelCard/{fuelcardId}/Fueltypes")]
        public ActionResult<GenericResult<GeneralModels>> AddFuelType(int fuelcardId, [FromBody] FuelTypeDTO fuel)
        {
            try
            {
                var check = _fuelCardManager.GetFuelCardById(fuelcardId);
                if (check.StatusCode != 200)
                {
                    return NotFound(check);
                }
                var result = _fuelCardManager.AddFuelType(fuelcardId,fuel);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("FuelCard/{fuelcardId}/Fueltypes/{fuelId}")]
        public ActionResult<GenericResult<GeneralModels>> DeleteFuelType(int fuelcardId, int fuelId)
        {
            try
            {
                var check = _fuelCardManager.GetFuelCardById(fuelcardId);
                if (check.StatusCode != 200)
                {
                    return NotFound(check);
                }
                var result = _fuelCardManager.DeleteFuelType(fuelcardId,fuelId);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("FuelCard/{fuelcardId}/ExtraServices")]
        public ActionResult<GenericResult<GeneralModels>> AddService(int fuelcardId, [FromBody] ExtraServiceDTO service)
        {
            try
            {
                var check = _fuelCardManager.GetFuelCardById(fuelcardId);
                if (check.StatusCode != 200)
                {
                    return NotFound(check);
                }
                var result = _fuelCardManager.AddService(service,fuelcardId);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("FuelCard/{fuelcardId}/ExtraServices/{serviceId}")]
        public ActionResult<GenericResult<GeneralModels>> DeleteService(int fuelcardId, int serviceId)
        {
            try
            {
                var check = _fuelCardManager.GetFuelCardById(fuelcardId);
                if (check.StatusCode != 200)
                {
                    return NotFound(check);
                }
                var result = _fuelCardManager.DeleteService(fuelcardId,serviceId);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("FuelCard/{fuelcardId}/Authentications")]
        public ActionResult<GenericResult<GeneralModels>> AddAuthentication(int fuelcardId, [FromBody] AuthenticationTypeDTO authentication)
        {
            try
            {
                var check = _fuelCardManager.GetFuelCardById(fuelcardId);
                if (check.StatusCode != 200)
                {
                    return NotFound(check);
                }
                var result = _fuelCardManager.AddAuthentication(authentication,fuelcardId);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("FuelCard/{fuelcardId}/Authentications/{authenticationId}")]
        public ActionResult<GenericResult<GeneralModels>> DeleteAuthentication(int fuelcardId, int authenticationId)
        {
            try
            {
                var check = _fuelCardManager.GetFuelCardById(fuelcardId);
                if (check.StatusCode != 200)
                {
                    return NotFound(check);
                }
                var result = _fuelCardManager.DeleteAuthentication(fuelcardId,authenticationId);
                return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
