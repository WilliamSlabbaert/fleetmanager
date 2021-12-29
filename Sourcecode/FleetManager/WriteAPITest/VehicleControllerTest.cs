using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models.general;
using BusinessLayer.models.input;
using BusinessLayer.validators.response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WriteAPI.Controllers;
using Xunit;

namespace WriteAPITest
{
    public class VehicleControllerTest
    {
        private Mock<ILogger<VehicleController>> _logger;
        private Mock<IMediator> _mediator;

        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();
        private VehicleController _controller;

        VehicleDTO tempVehicleDTO = new VehicleDTO()
        {
            Chassis= 0,
            Brand= "string",
            Model= "string",
            BuildDate= DateTime.Parse("2021-12-29T19:53:39.743Z"),
            Type= 0,
            FuelType= 0
        };
        LicensePlateDTO tempLicensePlateDTO = new LicensePlateDTO()
        {
            Plate = "oinzofen323",
            IsActive = true
        };
        KilometerHistoryDTO tempKilometerHistoryDTO = new KilometerHistoryDTO()
        {
            Kilometers = 111,
            Date = DateTime.Parse("2021-12-29T20:09:29.311Z")
        };
        public VehicleControllerTest()
        {
            this._logger = new Mock<ILogger<VehicleController>>();
            this._mediator = new Mock<IMediator>();
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);
            this._controller = new VehicleController(this._logger.Object,this._mediator.Object);
        }
        [Fact]
        public async void AddVehicleTest()
        {
            //Arrange 
            AddVehicleCommand query = new AddVehicleCommand(this.tempVehicleDTO);
            this._mediator.Setup(s => s.Send(It.IsAny<AddVehicleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");


            //Act
            var result = await this._controller.AddVehicle(tempVehicleDTO);
            var result1 = result.Result as OkObjectResult;
            var objectResult = result1.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result1);
            Assert.IsType<OkObjectResult>(result1);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public async void UpdateVehicleTest()
        {
            //Arrange 
            UpdateVehicleCommand query = new UpdateVehicleCommand(1,this.tempVehicleDTO);
            GetVehicleByIdQuery query1 = new GetVehicleByIdQuery(1);
            this._mediator.Setup(s => s.Send(It.IsAny<UpdateVehicleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");

            this._mediator.Setup(s => s.Send(It.IsAny<GetVehicleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");


            //Act
            var result = await this._controller.UpdateVehicle(1,tempVehicleDTO);
            var result1 = result.Result as OkObjectResult;
            var objectResult = result1.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result1);
            Assert.IsType<OkObjectResult>(result1);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public async void AddLicenseplateToVehicleTest()
        {
            //Arrange 
            AddLicensePlateToVehicleCommand query = new AddLicensePlateToVehicleCommand(1, this.tempLicensePlateDTO);
            GetVehicleByIdQuery query1 = new GetVehicleByIdQuery(1);
            this._mediator.Setup(s => s.Send(It.IsAny<AddLicensePlateToVehicleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");

            this._mediator.Setup(s => s.Send(It.IsAny<GetVehicleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");


            //Act
            var result = await this._controller.AddLicenseplateToVehicle(1, tempLicensePlateDTO);
            var result1 = result.Result as OkObjectResult;
            var objectResult = result1.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result1);
            Assert.IsType<OkObjectResult>(result1);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public async void PutLicenseplateToVehicleTest()
        {
            //Arrange 
            GetLicensePlateByIdQuery query = new GetLicensePlateByIdQuery(2);
            GetVehicleByIdQuery query1 = new GetVehicleByIdQuery(1);
            UpdateLicensePlateFromVehicleCommand query2 = new UpdateLicensePlateFromVehicleCommand(1,2,tempLicensePlateDTO);
            this._mediator.Setup(s => s.Send(It.IsAny<GetLicensePlateByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");

            this._mediator.Setup(s => s.Send(It.IsAny<GetVehicleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");
            this._mediator.Setup(s => s.Send(It.IsAny<UpdateLicensePlateFromVehicleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");


            //Act
            var result = await this._controller.PutLicenseplateToVehicle(1,2, tempLicensePlateDTO);
            var result1 = result.Result as OkObjectResult;
            var objectResult = result1.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result1);
            Assert.IsType<OkObjectResult>(result1);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public async void AddKilometersToVehicleTest()
        {
            //Arrange 
            AddKilometerHistoryCommand query = new AddKilometerHistoryCommand(1,tempKilometerHistoryDTO);
            GetVehicleByIdQuery query1 = new GetVehicleByIdQuery(1);
            this._mediator.Setup(s => s.Send(It.IsAny<AddKilometerHistoryCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");

            this._mediator.Setup(s => s.Send(It.IsAny<GetVehicleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");



            //Act
            var result = await this._controller.AddKilometersToVehicle(1,this.tempKilometerHistoryDTO);
            var result1 = result.Result as OkObjectResult;
            var objectResult = result1.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result1);
            Assert.IsType<OkObjectResult>(result1);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public async void DeleteKilometersToVehicleTest()
        {
            //Arrange 
            DeleteKilometerHistoryCommand query = new DeleteKilometerHistoryCommand(2,1);
            GetVehicleByIdQuery query1 = new GetVehicleByIdQuery(1);
            this._mediator.Setup(s => s.Send(It.IsAny<DeleteKilometerHistoryCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");

            this._mediator.Setup(s => s.Send(It.IsAny<GetVehicleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");



            //Act
            var result = await this._controller.DeleteKilometersToVehicle(1, 2);
            var result1 = result.Result as OkObjectResult;
            var objectResult = result1.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result1);
            Assert.IsType<OkObjectResult>(result1);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }

    }
}
