using BusinessLayer.models.general;
using BusinessLayer.services.interfaces;
using BusinessLayer.validators.response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Overall.paging;
using ReadAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReadAPITest
{
    public class FuelCardControllerTest
    {
        private Mock<IFuelCardService> _fuelcardService;
        private Mock<IChauffeurService> _chauffeurService;
        private Mock<ILogger<FuelCardController>> _logger;
        private FuelCardController _controller;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();

        public FuelCardControllerTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);
            this._fuelcardService = new Mock<IFuelCardService>();
            this._chauffeurService = new Mock<IChauffeurService>();
            this._logger = new Mock<ILogger<FuelCardController>>();
            this._controller = new FuelCardController(this._logger.Object, this._chauffeurService.Object, this._fuelcardService.Object);
        }
        [Fact]
        public void GetAllChauffeurTest()
        {
            //Arrange
            GenericParameter parameter = new GenericParameter();
            var metadata = new
            {
                TotalCount = 20,
                PageSize = 10,
                CurrentPage = 1,
                HasNext = true,
                HasPrevious = false
            };
            var httpContext = new DefaultHttpContext(); // or mock a `HttpContext`
            httpContext.Request.Headers["X-Pagination"] = JsonConvert.SerializeObject(metadata); //Set header
            this._controller = new FuelCardController(this._logger.Object, this._chauffeurService.Object,this._fuelcardService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            this._fuelcardService.Setup(s => s.GetHeaders(It.Is<GenericParameter>(s => s == parameter))).Returns(metadata);
            this._fuelcardService.Setup(s => s.GetAllFuelCardsPaging(It.Is<GenericParameter>(s => s == parameter))).Returns(response);

            //Act
            var result = this._controller.Get(parameter).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);

        }
        [Fact]
        public void GetChauffeurTest()
        {
            //Arrange
            int fuelcardId = 1;
            this._fuelcardService.Setup(s => s.GetFuelCardById(It.Is<int>(s => s == fuelcardId))).Returns(response);

            //Act
            var result = this._controller.GetFuelCardByID(fuelcardId).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
