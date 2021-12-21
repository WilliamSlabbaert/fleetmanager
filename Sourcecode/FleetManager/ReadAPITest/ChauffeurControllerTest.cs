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
using System.Net.Http;
using Xunit;

namespace ReadAPITest
{
    public class ChauffeurControllerTest
    {
        private Mock<IChauffeurService> _chauffeurService;
        private Mock<IFuelCardService> _fuelcardService;
        private Mock<ILogger<ChauffeurController>> _logger;
        private ChauffeurController _controller;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();

        public ChauffeurControllerTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);
            this._chauffeurService = new Mock<IChauffeurService>();
            this._logger = new Mock<ILogger<ChauffeurController>>();
            this._controller = new ChauffeurController(this._logger.Object,this._chauffeurService.Object);
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
            this._controller = new ChauffeurController(this._logger.Object, this._chauffeurService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            this._chauffeurService.Setup(s =>s.GetHeaders(It.Is<GenericParameter>(s =>s == parameter))).Returns(metadata);
            this._chauffeurService.Setup(s => s.GetAllChauffeursPaging(It.Is<GenericParameter>(s => s == parameter))).Returns(response);

            //Act
            var result = this._controller.GetAllChaffeurs(parameter).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK",objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);

        }
        [Fact]
        public void GetChauffeurTest()
        {
            //Arrange
            int ChauffeurId = 1;
            this._chauffeurService.Setup(s => s.GetChauffeurById(It.Is<int>(s => s == ChauffeurId))).Returns(response);

            //Act
            var result = this._controller.GetById(ChauffeurId).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
