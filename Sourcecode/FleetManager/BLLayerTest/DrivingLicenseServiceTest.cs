using AutoMapper;
using BusinessLayer;
using BusinessLayer.mediator.commands;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.models.input;
using BusinessLayer.services;
using BusinessLayer.validators;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BLLayerTest
{
    public class DrivingLicenseServiceTest
    {
        private Mock<IGenericRepo<DrivingLicenseEntity>> _drivingLicenseRepo;
        private Mock<IGenericRepo<ChauffeurEntity>> _chauffeurRepo;
        private Mock<IMapper> _mapper;
        private Mock<IMediator> _mediator;
        private Mock<IValidator<DrivingLicense>> _drivingLicenseValidator;
        private DrivingLicenseValidator validator = new DrivingLicenseValidator();
        private DrivingLicenseService _service;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();

        DrivingLicenseDTO tempDrivingLicenseDTO = new DrivingLicenseDTO()
        {
            type = Overall.License.B
        };

        public DrivingLicenseServiceTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);

            this._chauffeurRepo = new Mock<IGenericRepo<ChauffeurEntity>>();
            this._drivingLicenseRepo = new Mock<IGenericRepo<DrivingLicenseEntity>>();
            this._mapper = new Mock<IMapper>();
            this._mediator = new Mock<IMediator>();
            this._drivingLicenseValidator = new Mock<IValidator<DrivingLicense>>();

            this._service = new DrivingLicenseService(this._drivingLicenseRepo.Object, this._mapper.Object, this._chauffeurRepo.Object, this._mediator.Object, this.validator);
        }
        [Fact]
        public void AddDrivingLicenseTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<DrivingLicense>(It.IsAny<DrivingLicenseDTO>())).Returns(new DrivingLicense(type: Overall.License.B));
            this._mapper.Setup(x => x.Map<Chauffeur>(It.IsAny<ChauffeurEntity>())).Returns(new Chauffeur()
            {
                FirstName = "string",
                LastName = "string",
                City = "string",
                Street = "string",
                HouseNumber = "123",
                DateOfBirth = DateTime.Parse("1998-06-14T11:54:59.487Z"),
                NationalInsurenceNumber = "98.06.14-185.40",
                IsActive = true,
                DrivingLicenses = new List<DrivingLicense>()
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
                IsActive = true,
                DrivingLicenses = new List<DrivingLicenseEntity>()
            });
            this._chauffeurRepo.Setup(s => s.GetAll(null));
            this._chauffeurRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.AddDrivingLicense(tempDrivingLicenseDTO, 1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void DeleteDrivingLicenseTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<DrivingLicense>(It.IsAny<DrivingLicenseDTO>())).Returns(new DrivingLicense(type: Overall.License.B));
            this._mapper.Setup(x => x.Map<Chauffeur>(It.IsAny<ChauffeurEntity>())).Returns(new Chauffeur()
            {
                FirstName = "string",
                LastName = "string",
                City = "string",
                Street = "string",
                HouseNumber = "123",
                DateOfBirth = DateTime.Parse("1998-06-14T11:54:59.487Z"),
                NationalInsurenceNumber = "98.06.14-185.40",
                IsActive = true,
                DrivingLicenses = new List<DrivingLicense>()
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
                IsActive = true,
                DrivingLicenses = new List<DrivingLicenseEntity>()
            });
            this._chauffeurRepo.Setup(s => s.GetAll(null));
            this._chauffeurRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.DeleteDrivingLicense(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Drivinglicense doesn't exist in chaffeurs list.", result.Message);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void GetAllDrivingLicensesTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<List<DrivingLicense>>(It.IsAny<List<DrivingLicenseEntity>>()));
            CreateGenericResultCommand command = new CreateGenericResultCommand("OK", Overall.ResponseType.OK, null);
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");
            this._drivingLicenseRepo.Setup(s => s.GetAll(null));


            ////Act
            var result = this._service.GetAllDrivingLicenses();

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void GetAllDrivingLicensesPagingTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<List<DrivingLicense>>(It.IsAny<List<DrivingLicenseEntity>>()));
            CreateGenericResultCommand command = new CreateGenericResultCommand("OK", Overall.ResponseType.OK, null);
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");
            this._drivingLicenseRepo.Setup(s => s.GetAllWithPaging(It.IsAny<Func<IQueryable<DrivingLicenseEntity>, IIncludableQueryable<DrivingLicenseEntity, object>>>(), It.IsAny<GenericParameter>()));


            ////Act
            var result = this._service.GetAllDrivingLicensesPaging(new GenericParameter());

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void GetDrivingLicenseChaffeurByIdTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<List<DrivingLicense>>(It.IsAny<List<DrivingLicenseEntity>>()));
            CreateGenericResultCommand command = new CreateGenericResultCommand("OK", Overall.ResponseType.OK, null);
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");
            this._drivingLicenseRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<DrivingLicenseEntity, bool>>>(), It.IsAny<Func<IQueryable<DrivingLicenseEntity>, IIncludableQueryable<DrivingLicenseEntity, object>>>()));


            ////Act
            var result = this._service.GetDrivingLicenseChaffeurById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
