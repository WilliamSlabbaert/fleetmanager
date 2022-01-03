using AutoMapper;
using BusinessLayer;
using BusinessLayer.mediator.commands;
using BusinessLayer.models.general;
using BusinessLayer.models.input;
using BusinessLayer.services;
using BusinessLayer.validators;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Overall.paging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Xunit;

namespace BLLayerTest
{
    public class ChauffeurServiceTest
    {
        private Mock<IGenericRepo<ChauffeurEntity>> _chauffeurRepo;
        private Mock<IMapper> _mapper;
        private Mock<IGenericRepo<VehicleEntity>> _vehicleRepo;
        private Mock<IMediator> _mediator;
        private Mock<IValidator<Chauffeur>> _chauffeurValidator;
        private ChauffeurValidator validator = new ChauffeurValidator();
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();
        private ChauffeurService _service;

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

        public ChauffeurServiceTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);

            this._chauffeurRepo = new Mock<IGenericRepo<ChauffeurEntity>>();
            this._vehicleRepo = new Mock<IGenericRepo<VehicleEntity>>();
            this._mapper = new Mock<IMapper>();
            this._mediator = new Mock<IMediator>();
            this._chauffeurValidator = new Mock<IValidator<Chauffeur>>();

            this._service = new ChauffeurService(this._chauffeurRepo.Object, this._mapper.Object, this._vehicleRepo.Object, this._mediator.Object, this.validator);
        }

        [Fact]
        public void AddChauffeurTestInvalid()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Chauffeur>(It.IsAny<ChauffeurDTO>())).Returns(new Chauffeur());

            this._chauffeurRepo.Setup(s => s.AddEntity(It.IsAny<ChauffeurEntity>()));
            this._chauffeurRepo.Setup(s => s.GetAll(null));
            this._chauffeurRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.AddChauffeur(tempChauffeurDTO);

            //Assert
            Assert.NotNull(result);
            Assert.NotEqual("OK", result.Message);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void AddChauffeurTestValid()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Chauffeur>(It.IsAny<ChauffeurDTO>())).Returns(new Chauffeur() {
                FirstName = "string",
                LastName = "string",
                City = "string",
                Street = "string",
                HouseNumber = "123",
                DateOfBirth = DateTime.Parse("1998-06-14T11:54:59.487Z"),
                NationalInsurenceNumber = "98.06.14-185.40",
                IsActive = true
            });

            this._chauffeurRepo.Setup(s => s.AddEntity(It.IsAny<ChauffeurEntity>()));
            this._chauffeurRepo.Setup(s => s.GetAll(null));
            this._chauffeurRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.AddChauffeur(tempChauffeurDTO);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetChauffeurByIdTest()
        {
            //Arrange

            CreateGenericResultCommand command = new CreateGenericResultCommand("OK", Overall.ResponseType.OK, null);
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");
            this._chauffeurRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<ChauffeurEntity, bool>>>(),It.IsAny<Func<IQueryable<ChauffeurEntity>, IIncludableQueryable<ChauffeurEntity, object>>>()));
            this._chauffeurRepo.Setup(s => s.GetAll(null));
            this._chauffeurRepo.Setup(s => s.Save());

            //Act
            var result = this._service.GetChauffeurById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);

        }

        [Fact]
        public void UpdateChauffeurTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Chauffeur>(It.IsAny<ChauffeurDTO>())).Returns(new Chauffeur()
            {
                FirstName = "string",
                LastName = "string",
                City = "string",
                Street = "string",
                HouseNumber = "123",
                DateOfBirth = DateTime.Parse("1998-06-14T11:54:59.487Z"),
                NationalInsurenceNumber = "98.06.14-185.40",
                IsActive = true
            });
            this._chauffeurRepo.Setup(s => s.UpdateEntity(It.IsAny<ChauffeurEntity>()));
            this._chauffeurRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<ChauffeurEntity, bool>>>(), It.IsAny<Func<IQueryable<ChauffeurEntity>, IIncludableQueryable<ChauffeurEntity, object>>>())).Returns(new ChauffeurEntity()
            {
                FirstName = "string",
                LastName = "string",
                City = "string",
                Street = "string",
                HouseNumber = "123",
                DateOfBirth = DateTime.Parse("1998-06-14T11:54:59.487Z"),
                NationalInsurenceNumber = "98.06.14-185.40",
                IsActive = true
            });
            this._chauffeurRepo.Setup(s => s.GetAll(null));
            this._chauffeurRepo.Setup(s => s.Save());

            //Act
            var result = this._service.UpdateChauffeur(tempChauffeurDTO,1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);

        }

        [Fact]
        public void AddVehicleToChauffeurTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Chauffeur>(It.IsAny<ChauffeurEntity>())).Returns(new Chauffeur()
            {
                FirstName = "string",
                LastName = "string",
                City = "string",
                Street = "string",
                HouseNumber = "123",
                DateOfBirth = DateTime.Parse("1998-06-14T11:54:59.487Z"),
                NationalInsurenceNumber = "98.06.14-185.40",
                IsActive = true
            });
            this._chauffeurRepo.Setup(s => s.UpdateEntity(It.IsAny<ChauffeurEntity>()));
            this._chauffeurRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<ChauffeurEntity, bool>>>(), It.IsAny<Func<IQueryable<ChauffeurEntity>, IIncludableQueryable<ChauffeurEntity, object>>>())).Returns(new ChauffeurEntity()
            {
                FirstName = "string",
                LastName = "string",
                City = "string",
                Street = "string",
                HouseNumber = "123",
                DateOfBirth = DateTime.Parse("1998-06-14T11:54:59.487Z"),
                NationalInsurenceNumber = "98.06.14-185.40",
                IsActive = true
            });

            this._vehicleRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<VehicleEntity, bool>>>(), It.IsAny<Func<IQueryable<VehicleEntity>, IIncludableQueryable<VehicleEntity, object>>>())).Returns(new VehicleEntity()
            {
                Chassis = 0,
                Brand = "string",
                Model = "string",
                BuildDate = DateTime.Parse("2021-12-29T19:53:39.743Z"),
                Type = 0,
                FuelType = 0
            });
            this._chauffeurRepo.Setup(s => s.GetAll(null));
            this._chauffeurRepo.Setup(s => s.Save());

            //Act
            var result = this._service.AddVehicleToChauffeur( 1,2);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);

        }

        [Fact]
        public void UpdateVehicleToChauffeurTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Chauffeur>(It.IsAny<ChauffeurEntity>())).Returns(new Chauffeur()
            {
                FirstName = "string",
                LastName = "string",
                City = "string",
                Street = "string",
                HouseNumber = "123",
                DateOfBirth = DateTime.Parse("1998-06-14T11:54:59.487Z"),
                NationalInsurenceNumber = "98.06.14-185.40",
                IsActive = true
            });
            this._chauffeurRepo.Setup(s => s.UpdateEntity(It.IsAny<ChauffeurEntity>()));
            this._chauffeurRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<ChauffeurEntity, bool>>>(), It.IsAny<Func<IQueryable<ChauffeurEntity>, IIncludableQueryable<ChauffeurEntity, object>>>())).Returns(new ChauffeurEntity()
            {
                FirstName = "string",
                LastName = "string",
                City = "string",
                Street = "string",
                HouseNumber = "123",
                DateOfBirth = DateTime.Parse("1998-06-14T11:54:59.487Z"),
                NationalInsurenceNumber = "98.06.14-185.40",
                IsActive = true
            });

            this._vehicleRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<VehicleEntity, bool>>>(), It.IsAny<Func<IQueryable<VehicleEntity>, IIncludableQueryable<VehicleEntity, object>>>())).Returns(new VehicleEntity()
            {
                Chassis = 0,
                Brand = "string",
                Model = "string",
                BuildDate = DateTime.Parse("2021-12-29T19:53:39.743Z"),
                Type = 0,
                FuelType = 0
            });
            this._chauffeurRepo.Setup(s => s.GetAll(null));
            this._chauffeurRepo.Setup(s => s.Save());

            //Act
            var result = this._service.UpdateVehicleToChauffeur(1, 2,true);

            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public void GetAllChauffeurByIdTest()
        {
            //Arrange

            CreateGenericResultCommand command = new CreateGenericResultCommand("OK", Overall.ResponseType.OK, null);
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");
            GenericParameter parameter = new GenericParameter();
            this._chauffeurRepo.Setup(s => s.GetAllWithPaging(It.IsAny<Func<IQueryable<ChauffeurEntity>, IIncludableQueryable<ChauffeurEntity, object>>>(), It.IsAny<GenericParameter>()));
            this._chauffeurRepo.Setup(s => s.GetAll(null));
            this._chauffeurRepo.Setup(s => s.Save());

            //Act
            var result = this._service.GetAllChauffeursPaging(parameter);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);

        }
    }
}
