using BusinessLayer.models.general;
using BusinessLayer.models.input;
using BusinessLayer.services.interfaces;
using BusinessLayer.validators.response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteAPI.Controllers;
using Xunit;

namespace WriteAPITest
{
    public class RequestControllerTest
    {
        private Mock<ILogger<RequestController>> _logger;
        private Mock<IMediator> _mediator;
        private Mock<IRequestService> _requestService;
        private Mock<IMaintenanceService> _maintenanceService;
        private Mock<IRepairmentService> _repairmentService;
        private RequestController _controller;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();
        RequestDTO tempRequestDTO = new RequestDTO()
        {
            StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
            EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
            Status= "test",
            Type = Overall.RequestType.Fuelcard
        };
        MaintenanceDTO tempMaintenanceDTO = new MaintenanceDTO()
        {
            Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
            Price = 10,
            Garage = "Garage"
        };
        RepairmentDTO tempRepairmentDTO = new RepairmentDTO()
        {
            Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
            Description = "test",
            Company = "Garage"
        };


        public RequestControllerTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);
            this._logger = new Mock<ILogger<RequestController>>();
            this._mediator = new Mock<IMediator>();
            this._requestService = new Mock<IRequestService>();
            this._maintenanceService = new Mock<IMaintenanceService>();
            this._repairmentService = new Mock<IRepairmentService>();
            this._controller = new RequestController(this._logger.Object,this._mediator.Object,this._requestService.Object,this._maintenanceService.Object,this._repairmentService.Object);
        }

        [Fact]
        public void updateRequestTest()
        {
            //Arrange
            this._requestService.Setup(s => s.GetRequestById(It.Is<int>(s => s == 1))).Returns(response);
            this._requestService.Setup(s => s.UpdateRequest(It.Is<RequestDTO>(s => s == tempRequestDTO), It.Is<int>(s => s == 1))).Returns(response);


            //Act
            var result = this._controller.UpdateRequest(1, tempRequestDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void addMaintenanceTest()
        {
            //Arrange
            this._requestService.Setup(s => s.GetRequestById(It.Is<int>(s => s == 1))).Returns(response);
            this._maintenanceService.Setup(s => s.AddMaintenance(It.Is<MaintenanceDTO>(s => s == tempMaintenanceDTO), It.Is<int>(s => s == 1))).Returns(response);


            //Act
            var result = this._controller.AddMaintenance(1, tempMaintenanceDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void deleteMaintenanceTest()
        {
            //Arrange
            this._requestService.Setup(s => s.GetRequestById(It.Is<int>(s => s == 1))).Returns(response);
            this._maintenanceService.Setup(s => s.GetMaintenanceById(It.Is<int>(s => s == 2))).Returns(response);
            this._maintenanceService.Setup(s => s.DeleteMaintenance(It.Is<int>(s => s == 1),It.Is<int>(s => s == 2))).Returns(response);


            //Act
            var result = this._controller.DeleteMaintenance(1, 2).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void addRepairTest()
        {
            //Arrange
            this._requestService.Setup(s => s.GetRequestById(It.Is<int>(s => s == 1))).Returns(response);
            this._repairmentService.Setup(s => s.AddRepairment(It.Is<RepairmentDTO>(s => s == tempRepairmentDTO),It.Is<int>(s => s == 1))).Returns(response);


            //Act
            var result = this._controller.AddRepair(1, tempRepairmentDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void deleteRepairTest()
        {
            //Arrange
            this._requestService.Setup(s => s.GetRequestById(It.Is<int>(s => s == 1))).Returns(response);
            this._repairmentService.Setup(s => s.GetRepairmentById(It.Is<int>(s => s == 2))).Returns(response);
            this._repairmentService.Setup(s => s.DeleteRepairment(It.Is<int>(s => s == 1), It.Is<int>(s => s == 2))).Returns(response);


            //Act
            var result = this._controller.DeleteRepair(1, 2).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }

    }
}
