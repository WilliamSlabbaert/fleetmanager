using BusinessLayer.mediator.queries;
using BusinessLayer.models.general;
using BusinessLayer.services.interfaces;
using BusinessLayer.validators.response;
using MediatR;
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
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ReadAPITest
{
    public class VehicleControllerTest
    {
        private Mock<IMediator> _mediator;
        private Mock<ILogger<VehicleController>> _logger;
        private Mock<IVehicleService> _vehicleService;
        private VehicleController _controller;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();

        public VehicleControllerTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);
            this._mediator = new Mock<IMediator>();
            this._logger = new Mock<ILogger<VehicleController>>();
            this._vehicleService = new Mock<IVehicleService>();
            this._controller = new VehicleController(this._logger.Object,this._mediator.Object, this._vehicleService.Object);
        }
        [Fact]
        public void GetAllVehiclesTest()
        {
            //Arrange 
            GenericParameter parameter = new GenericParameter();
            GetVehiclesPagingQuery query = new GetVehiclesPagingQuery(parameter);
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
            this._controller = new VehicleController(this._logger.Object, this._mediator.Object, this._vehicleService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };
            this._mediator.Setup(s => s.Send(It.IsAny<GetVehiclesPagingQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");


            //Act
            var result = this._controller.GetAllVehicles(parameter).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void GetVehicleTest()
        {
            //Arrange 
            int id = 1;
            GetVehicleByIdQuery query = new GetVehicleByIdQuery(id);
           
            this._mediator.Setup(s => s.Send(It.IsAny<GetVehicleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");


            //Act
            var result = this._controller.GetVehicleByID(id).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
