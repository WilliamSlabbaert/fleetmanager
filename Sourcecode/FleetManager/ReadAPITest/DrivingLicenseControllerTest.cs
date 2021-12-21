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
    public class DrivingLicenseControllerTest
    {
        private Mock<IDrivingLicenseService> _drivingLicenseService;
        private Mock<IChauffeurService> _chauffeurService;
        private Mock<ILogger<DrivingLicenseController>> _logger;
        private DrivingLicenseController _controller;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();

        public DrivingLicenseControllerTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);
            this._drivingLicenseService = new Mock<IDrivingLicenseService>();
            this._chauffeurService = new Mock<IChauffeurService>();
            this._logger = new Mock<ILogger<DrivingLicenseController>>();
            this._controller = new DrivingLicenseController(this._logger.Object, this._chauffeurService.Object, this._drivingLicenseService.Object);
        }
        [Fact]
        public void GetAllDrivingLicensesTest()
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
            this._controller = new DrivingLicenseController(this._logger.Object, this._chauffeurService.Object, this._drivingLicenseService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            this._drivingLicenseService.Setup(s => s.GetHeaders(It.Is<GenericParameter>(s => s == parameter))).Returns(metadata);
            this._drivingLicenseService.Setup(s => s.GetAllDrivingLicensesPaging(It.Is<GenericParameter>(s => s == parameter))).Returns(response);

            //Act
            var result = this._controller.GetAll(parameter).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);

        }
        [Fact]
        public void GetDrivingLicenseTest()
        {
            //Arrange
            int id = 1;
            this._drivingLicenseService.Setup(s => s.GetAllDrivingLicenseById(It.Is<int>(s => s == id))).Returns(response);

            //Act
            var result = this._controller.GetFuelCardByID(id).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
