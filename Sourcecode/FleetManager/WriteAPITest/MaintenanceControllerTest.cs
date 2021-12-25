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
    public class MaintenanceControllerTest
    {
        private Mock<ILogger<MaintenanceController>> _logger;
        private Mock<IMediator> _mediator;
        private Mock<IRequestService> _requestService;
        private Mock<IMaintenanceService> _maintenanceService;
        private Mock<IRepairmentService> _repairmentService;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();
        private MaintenanceController _controller;

        MaintenanceDTO tempMaintenanceDTO = new MaintenanceDTO()
        {
            Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
            Price = 10,
            Garage = "Garage"
        };
        InvoiceDTO tempInvoiceDTO = new InvoiceDTO()
        {
           InvoiceImage = "image"
        };


        public MaintenanceControllerTest()
        {
            this._logger = new Mock<ILogger<MaintenanceController>>();
            this._requestService = new Mock<IRequestService>();
            this._mediator = new Mock<IMediator>();
            this._maintenanceService = new Mock<IMaintenanceService>();
            this._repairmentService = new Mock<IRepairmentService>();
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);
            this._controller = new MaintenanceController(this._logger.Object, this._mediator.Object,this._requestService.Object,this._maintenanceService.Object,this._repairmentService.Object);
        }
        [Fact]
        public void UpdateMaintenanceTest()
        {

            //Arrange
            this._maintenanceService.Setup(s => s.GetMaintenanceById(It.Is<int>(s => s == 1))).Returns(response);
            this._maintenanceService.Setup(s => s.UpdateMaintenance(It.Is<int>(s => s == 1),It.Is<MaintenanceDTO>(s => s == tempMaintenanceDTO))).Returns(response);


            //Act
            var result = this._controller.UpdateMaintenance(1,tempMaintenanceDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void AddInvoiceTest()
        {

            //Arrange
            this._maintenanceService.Setup(s => s.GetMaintenanceById(It.Is<int>(s => s == 1))).Returns(response);
            this._maintenanceService.Setup(s => s.AddInvoice(It.Is<int>(s => s == 1), It.Is<InvoiceDTO>(s => s == tempInvoiceDTO))).Returns(response);


            //Act
            var result = this._controller.AddInvoice(1, tempInvoiceDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void DeleteInvoiceTest()
        {

            //Arrange
            this._maintenanceService.Setup(s => s.GetMaintenanceById(It.Is<int>(s => s == 1))).Returns(response);
            this._maintenanceService.Setup(s => s.DeleteInvoice(It.Is<int>(s => s == 1), It.Is<int>(s => s == 2))).Returns(response);


            //Act
            var result = this._controller.DeleteInvoice(1, 2).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
