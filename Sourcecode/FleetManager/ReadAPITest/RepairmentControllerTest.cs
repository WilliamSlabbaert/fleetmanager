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
    public class RepairmentControllerTest
    {
        private Mock<IRequestService> _requestService;
        private Mock<IRepairmentService> _repairmentService;
        private Mock<ILogger<RepairmentController>> _logger;
        private RepairmentController _controller;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();

        public RepairmentControllerTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);
            this._requestService = new Mock<IRequestService>();
            this._repairmentService = new Mock<IRepairmentService>();
            this._logger = new Mock<ILogger<RepairmentController>>();
            this._controller = new RepairmentController(this._logger.Object,this._repairmentService.Object, this._requestService.Object);
        }
        [Fact]
        public void GetAllRepairementTest()
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
            this._controller = new RepairmentController(this._logger.Object, this._repairmentService.Object, this._requestService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            this._repairmentService.Setup(s => s.GetHeaders(It.Is<GenericParameter>(s => s == parameter))).Returns(metadata);
            this._repairmentService.Setup(s => s.GetAllRepairmentsPaging(It.Is<GenericParameter>(s => s == parameter))).Returns(response);

            //Act
            var result = this._controller.Getall(parameter).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);

        }
        [Fact]
        public void GetRepairementTest()
        {
            //Arrange
            int id = 1;
            this._repairmentService.Setup(s => s.GetRepairmentById(It.Is<int>(s => s == id))).Returns(response);

            //Act
            var result = this._controller.GetById(id).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
