using BusinessLayer.mediator.queries;
using BusinessLayer.models.general;
using BusinessLayer.models.input;
using BusinessLayer.services.interfaces;
using BusinessLayer.validators.response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using WriteAPI.Controllers;
using Xunit;

namespace WriteAPITest
{
    public class ChauffeurControllerTest
    {

        private Mock<IChauffeurService> _chauffeurService;
        private Mock<IFuelCardService> _fuelcardService;
        private Mock<IRequestService> _requestService;
        private Mock<IDrivingLicenseService> _drivingLicenseService;
        private Mock<IMediator> _mediator;
        private Mock<ILogger<ChauffeurController>> _logger;
        private ChauffeurController _controller;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();
        ChauffeurDTO tempChauffeurDTO = new ChauffeurDTO()
        {
            FirstName = "string",
            LastName = "string",
            City = "string",
            Street = "string",
            HouseNumber = "123",
            DateOfBirth = DateTime.Parse("1998-06-14T11:54:59.487Z"),
            NationalInsurenceNumber = "98.06.14-185.40",
            IsActive = true

        };
        DrivingLicenseDTO tempDrivingLicenseDTO = new DrivingLicenseDTO()
        {
            type = Overall.License.B
        };
        RequestDTO tempRequestDTO = new RequestDTO()
        {
            StartDate = DateTime.Parse("2021-12-23T14:43:05.956Z"),
            EndDate = DateTime.Parse("2021-12-23T14:43:05.956Z"),
            Status = "string",
            Type = 0
        };

        public ChauffeurControllerTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);
            this._chauffeurService = new Mock<IChauffeurService>();
            this._logger = new Mock<ILogger<ChauffeurController>>();
            this._fuelcardService = new Mock<IFuelCardService>();
            this._mediator = new Mock<IMediator>();
            this._drivingLicenseService = new Mock<IDrivingLicenseService>();
            this._requestService = new Mock<IRequestService>();
            this._controller = new ChauffeurController(this._logger.Object, this._chauffeurService.Object, this._drivingLicenseService.Object, this._fuelcardService.Object, _mediator.Object, this._requestService.Object);
        }
        [Fact]
        public void AddChauffeurTest()
        {

            //Arrange
            this._chauffeurService.Setup(s => s.AddChauffeur(It.Is<ChauffeurDTO>(s => s == tempChauffeurDTO))).Returns(response);

            //Act
            var result = this._controller.Add(tempChauffeurDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void UpdateChauffeurTest()
        {

            this._chauffeurService.Setup(s => s.UpdateChauffeur(It.Is<ChauffeurDTO>(s => s == tempChauffeurDTO), It.Is<int>(s => s == 1))).Returns(response);
            this._chauffeurService.Setup(s => s.GetChauffeurById(It.Is<int>(s => s == 1))).Returns(response);

            //Act
            var result = this._controller.UpdateById(1, tempChauffeurDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void AddVehicleToChauffeurTest()
        {

            //Arrange


            this._mediator.Setup(s => s.Send(It.IsAny<GetVehicleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");

            this._chauffeurService.Setup(s => s.AddVehicleToChauffeur(It.Is<int>(s => s == 2), It.Is<int>(s => s == 1))).Returns(response);
            this._chauffeurService.Setup(s => s.GetChauffeurById(It.Is<int>(s => s == 2))).Returns(response);

            //Act
            var result = this._controller.AddVehicleToChauffeur(2, 1).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void PatchVehicleToChauffeurTest()
        {

            //Arrange

            this._mediator.Setup(s => s.Send(It.IsAny<GetVehicleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");

            this._chauffeurService.Setup(s => s.UpdateVehicleToChauffeur(It.Is<int>(s => s == 2), It.Is<int>(s => s == 1), It.Is<bool>(s => s == true))).Returns(response);
            this._chauffeurService.Setup(s => s.GetChauffeurById(It.Is<int>(s => s == 2))).Returns(response);

            //Act
            var result = this._controller.UpdateVehicleToChauffeur(2, 1, true).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void AddDrivinglicenseTest()
        {

            //Arrange

            this._mediator.Setup(s => s.Send(It.IsAny<GetVehicleByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");

            this._drivingLicenseService.Setup(s => s.AddDrivingLicense(It.Is<DrivingLicenseDTO>(s => s == tempDrivingLicenseDTO), It.Is<int>(s => s == 2))).Returns(response);
            this._chauffeurService.Setup(s => s.GetChauffeurById(It.Is<int>(s => s == 2))).Returns(response);

            //Act
            var result = this._controller.AddDrivinglicense(2, tempDrivingLicenseDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void DeleteDrivinglicensesByIDTest()
        {

            //Arrange

            this._drivingLicenseService.Setup(s => s.DeleteDrivingLicense(It.Is<int>(s => s == 1), It.Is<int>(s => s == 2))).Returns(response);
            this._chauffeurService.Setup(s => s.GetChauffeurById(It.Is<int>(s => s == 2))).Returns(response);
            this._drivingLicenseService.Setup(s => s.GetAllDrivingLicenseById(1)).Returns(response);

            //Act
            var result = this._controller.DeleteDrivinglicensesByID(2, 1).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void AddFuelcardTest()
        {

            //Arrange
            this._chauffeurService.Setup(s => s.GetChauffeurById(It.Is<int>(s => s == 2))).Returns(response);
            this._fuelcardService.Setup(s => s.GetFuelCardById(It.Is<int>(s => s == 1))).Returns(response);
            this._fuelcardService.Setup(s => s.AddFuelCardToChauffeur(It.Is<int>(s => s == 1), It.Is<int>(s => s == 2))).Returns(response);
            //Act
            var result = this._controller.AddFuelCard(2, 1).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void UpdateFuelCardActivityTest()
        {

            //Arrange
            this._chauffeurService.Setup(s => s.GetChauffeurById(It.Is<int>(s => s == 2))).Returns(response);
            this._fuelcardService.Setup(s => s.GetFuelCardById(It.Is<int>(s => s == 1))).Returns(response);
            this._fuelcardService.Setup(s => s.ActivityChauffeurFuelCard(It.Is<int>(s => s == 1), It.Is<int>(s => s == 2), It.Is<bool>(s => s == true))).Returns(response);
            //Act
            var result = this._controller.UpdateFuelCardActivity(2, 1, true).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void AddRequestTest()
        {

            //Arrange
            this._chauffeurService.Setup(s => s.GetChauffeurById(It.Is<int>(s => s == 2))).Returns(response);
            this._requestService.Setup(s => s.AddRequest(It.Is<RequestDTO>(s => s == tempRequestDTO), It.Is<int>(s => s == 2), It.Is<int>(s => s == 1))).Returns(response);


            this._mediator.Setup(s => s.Send(It.IsAny<GetVehicleByIdFromChauffeurQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");
            //Act
            var result = this._controller.AddRequest(2, 1, tempRequestDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
