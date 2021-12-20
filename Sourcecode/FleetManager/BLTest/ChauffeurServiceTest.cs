using BusinessLayer.models.general;
using BusinessLayer.services.interfaces;
using BusinessLayer.validators.response;
using Moq;
using Overall.paging;
using System;
using Xunit;

namespace BLTest
{
    public class ChauffeurServiceTest
    {
        private Mock<IChauffeurService> _chauffeurService;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();

        public ChauffeurServiceTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);
            this._chauffeurService = new Mock<IChauffeurService>();

        }

        [Fact]
        public void GetAllChauffeurTest()
        {
            //Arrange
            GenericParameter parameter = new GenericParameter();
            this._chauffeurService.Setup(s => s.GetAllChauffeursPaging(It.Is<GenericParameter>(s => s == parameter))).Returns(response);

            //Act
            var result = this._chauffeurService.Object.GetAllChauffeursPaging(parameter);

            //Assert
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result);
        }
        [Fact]
        public void GetChauffeurTest()
        {
            //Arrange
            int ChauffeurId = 1;
            this._chauffeurService.Setup(s => s.GetChauffeurById(It.Is<int>(s => s == ChauffeurId))).Returns(response);

            //Act
            var result = this._chauffeurService.Object.GetChauffeurById(ChauffeurId);

            //Assert
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result);
        }
    }
}
