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
    public class RequestControllerTest
    {

        private Mock<IRequestService> _requestService;
        private Mock<ILogger<RequestController>> _logger;
        private RequestController _controller;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();

        public RequestControllerTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);
            this._requestService = new Mock<IRequestService>();
            this._logger = new Mock<ILogger<RequestController>>();
            this._controller = new RequestController(this._logger.Object,this._requestService.Object);
        }
        [Fact]
        public void GetAllRequestsTest()
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
            this._controller = new RequestController(this._logger.Object, this._requestService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            this._requestService.Setup(s => s.GetHeaders(It.Is<GenericParameter>(s => s == parameter))).Returns(metadata);
            this._requestService.Setup(s => s.GetAllRequestsPaging(It.Is<GenericParameter>(s => s == parameter))).Returns(response);

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
        public void GetRequestTest()
        {
            //Arrange
            int id = 1;
            this._requestService.Setup(s => s.GetRequestById(It.Is<int>(s => s == id))).Returns(response);

            //Act
            var result = this._controller.GetById(id).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void GetRequestMaintenanceTest()
        {
            //Arrange
            int id = 1;
            this._requestService.Setup(s => s.GetRequestMaintenance(It.Is<int>(s => s == id))).Returns(response);

            //Act
            var result = this._controller.GetByIdMaintenance(id).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void GetRequestRepairmentTest()
        {
            //Arrange
            int id = 1;
            this._requestService.Setup(s => s.GetRequestRepairs(It.Is<int>(s => s == id))).Returns(response);

            //Act
            var result = this._controller.GetByIdRepairments(id).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void GetRequestVehicleTest()
        {
            //Arrange
            int id = 1;
            this._requestService.Setup(s => s.GetRequestVehicle(It.Is<int>(s => s == id))).Returns(response);

            //Act
            var result = this._controller.GetByIdVehicle(id).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void GetRequestChauffeurTest()
        {
            //Arrange
            int id = 1;
            this._requestService.Setup(s => s.GetRequestChaffeur(It.Is<int>(s => s == id))).Returns(response);

            //Act
            var result = this._controller.GetByIdChaffeur(id).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
