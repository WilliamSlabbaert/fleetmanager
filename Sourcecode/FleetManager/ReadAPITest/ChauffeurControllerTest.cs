using BusinessLayer.models.general;
using BusinessLayer.services.interfaces;
using BusinessLayer.validators.response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Overall.paging;
using ReadAPI.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ReadAPITest
{
    public class ChauffeurControllerTest
    {
        private readonly Mock<IChauffeurService> _chauffeurService;
        private readonly ChauffeurController _chauffeurController;
        private readonly GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();
        private readonly Mock<IMediator> _mediator;


        public ChauffeurControllerTest()
        {
            this.response.Message = "Nie";
            this.response.SetStatusCode(Overall.ResponseType.OK);
            this._chauffeurService = new Mock<IChauffeurService>();
            this._mediator = new Mock<IMediator>();
            this._chauffeurController = new ChauffeurController(this._chauffeurService.Object);

        }

        [Fact]
        public void GetAllChauffeurTest()
        {
            //Arrange
            GenericParameter parameter = new GenericParameter();
            this._chauffeurService.Setup(s => s.GetAllChauffeursPaging(It.Is<GenericParameter>(s => s == parameter))).Returns(response);

            //Act
            var result = this._chauffeurController.GetAllChaffeurs(parameter);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public void GetChauffeurTest()
        {
            //Arrange
            int ChauffeurId = 1;
            this._chauffeurService.Setup(s => s.GetChauffeurById(It.Is<int>(s => s == 1))).Returns(response);

            //Act
            var result = this._chauffeurController.GetById(ChauffeurId);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);


        }
    }
}
