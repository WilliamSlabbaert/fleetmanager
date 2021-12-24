using BusinessLayer.models.general;
using BusinessLayer.models.input;
using BusinessLayer.services.interfaces;
using BusinessLayer.validators.response;
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
    public class FuelCardControllerTest
    {
        private Mock<ILogger<FuelCardController>> _logger;
        private Mock<IFuelCardService> _fuelCardManager;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();
        private FuelCardController _controller;

        FuelCardDTO tempFuelCardDTO = new FuelCardDTO()
        {
            CardNumber = "string",
            Pin = "string",
            ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
            IsActive = true
        };
        FuelTypeDTO tempFuelTypeDTO = new FuelTypeDTO()
        {
            Fuel = Overall.FuelTypes.Diesel
        };
        ExtraServiceDTO tempServiceDTO = new ExtraServiceDTO()
        {
            Service = Overall.ExtraServices.DiscountCarwash
        };
        AuthenticationTypeDTO tempAuthenticationTypeDTO = new AuthenticationTypeDTO()
        {
            type = Overall.AuthenticationTypes.PIN
        };

        public FuelCardControllerTest()
        {
            this._logger = new Mock<ILogger<FuelCardController>>();
            this._fuelCardManager = new Mock<IFuelCardService>();
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);
            this._controller = new FuelCardController(this._logger.Object, this._fuelCardManager.Object);
        }
        [Fact]
        public void AddFuelCardTest()
        {
            //Arrange
            this._fuelCardManager.Setup(s => s.AddFuelCard(It.Is<FuelCardDTO>(s => s == tempFuelCardDTO))).Returns(response);

            //Act
            var result = this._controller.AddFuelCard(tempFuelCardDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void UpdateFuelCardTest()
        {
            //Arrange
            this._fuelCardManager.Setup(s => s.UpdateFuelCard(It.Is<int>(s => s == 1), It.Is<FuelCardDTO>(s => s == tempFuelCardDTO))).Returns(response);
            this._fuelCardManager.Setup(s => s.GetFuelCardById(It.Is<int>(s => s == 1))).Returns(response);

            //Act
            var result = this._controller.updateFuelCard(1,tempFuelCardDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void AddFuelTypeTest()
        {
            //Arrange
            this._fuelCardManager.Setup(s => s.AddFuelType(It.Is<int>(s => s == 1), It.Is<FuelTypeDTO>(s => s == tempFuelTypeDTO))).Returns(response);
            this._fuelCardManager.Setup(s => s.GetFuelCardById(It.Is<int>(s => s == 1))).Returns(response);

            //Act
            var result = this._controller.AddFuelType(1, tempFuelTypeDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void DeleteFuelTypeTest()
        {
            //Arrange
            this._fuelCardManager.Setup(s => s.DeleteFuelType(It.Is<int>(s => s == 1), It.Is<int>(s => s == 2))).Returns(response);
            this._fuelCardManager.Setup(s => s.GetFuelCardById(It.Is<int>(s => s == 1))).Returns(response);

            //Act
            var result = this._controller.DeleteFuelType(1, 2).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void AddServiceTest()
        {
            //Arrange
            this._fuelCardManager.Setup(s => s.AddService(It.Is<ExtraServiceDTO>(s => s == tempServiceDTO), It.Is<int>(s => s == 1))).Returns(response);
            this._fuelCardManager.Setup(s => s.GetFuelCardById(It.Is<int>(s => s == 1))).Returns(response);

            //Act
            var result = this._controller.AddService(1,tempServiceDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void DeleteServiceTest()
        {
            //Arrange
            this._fuelCardManager.Setup(s => s.DeleteService(It.Is<int>(s => s == 1), It.Is<int>(s => s == 2))).Returns(response);
            this._fuelCardManager.Setup(s => s.GetFuelCardById(It.Is<int>(s => s == 1))).Returns(response);

            //Act
            var result = this._controller.DeleteService(1,2).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void AddAuthenticationTest()
        {
            //Arrange
            this._fuelCardManager.Setup(s => s.AddAuthentication(It.Is<AuthenticationTypeDTO>(s => s == tempAuthenticationTypeDTO), It.Is<int>(s => s == 2))).Returns(response);
            this._fuelCardManager.Setup(s => s.GetFuelCardById(It.Is<int>(s => s == 2))).Returns(response);

            //Act
            var result = this._controller.AddAuthentication(2,tempAuthenticationTypeDTO).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public void DeleteAuthenticationTest()
        {
            //Arrange
            this._fuelCardManager.Setup(s => s.DeleteAuthentication(It.Is<int>(s => s == 2), It.Is<int>(s => s == 1))).Returns(response);
            this._fuelCardManager.Setup(s => s.GetFuelCardById(It.Is<int>(s => s == 2))).Returns(response);

            //Act
            var result = this._controller.DeleteAuthentication(2, 1).Result as ObjectResult;
            var objectResult = result.Value as GenericResult<GeneralModels>;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OK", objectResult.Message);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
