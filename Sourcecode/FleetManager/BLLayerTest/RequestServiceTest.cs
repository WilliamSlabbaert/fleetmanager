using AutoMapper;
using BusinessLayer.mediator.commands;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.models.input;
using BusinessLayer.services;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
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
    public class RequestServiceTest
    {
        private Mock<IGenericRepo<RequestEntity>> _requestRepo;
        private Mock<IGenericRepo<ChauffeurEntity>> _chauffeurRepo;
        private Mock<IGenericRepo<VehicleEntity>> _vehicleRepo;
        private Mock<IMapper> _mapper;
        private Mock<IMediator> _mediator;
        private Mock<IValidator<Request>> _requestValidator;

        private RequestService _service;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();

        RequestDTO tempRequestDTO = new RequestDTO()
        {
            StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
            EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
            Status = "test",
            Type = Overall.RequestType.Fuelcard
        };
        RepairmentDTO tempRepairmentDTO = new RepairmentDTO()
        {
            Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
            Description = "test",
            Company = "Garage"
        };
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
        VehicleDTO tempVehicleDTO = new VehicleDTO()
        {
            Chassis = 0,
            Brand = "string",
            Model = "string",
            BuildDate = DateTime.Parse("2021-12-29T19:53:39.743Z"),
            Type = 0,
            FuelType = 0
        };
        public RequestServiceTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);

            this._requestRepo = new Mock<IGenericRepo<RequestEntity>>();
            this._vehicleRepo = new Mock<IGenericRepo<VehicleEntity>>();
            this._chauffeurRepo = new Mock<IGenericRepo<ChauffeurEntity>>();
            this._mapper = new Mock<IMapper>();
            this._mediator = new Mock<IMediator>();
            this._requestValidator = new Mock<IValidator<Request>>();

            this._service = new RequestService(this._requestRepo.Object, this._mapper.Object, this._chauffeurRepo.Object, this._vehicleRepo.Object, this._mediator.Object, new BusinessLayer.validators.RequestValidator());
        }
        [Fact]
        public void AddRequestTest()
        {
            //Arrange

            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");

            this._mapper.Setup(x => x.Map<Request>(It.IsAny<RequestDTO>())).Returns(new Request(DateTime.Parse("2021-12-25T13:20:56.337Z"), DateTime.Parse("2021-12-26T13:20:56.337Z"), "test", Overall.RequestType.Fuelcard)
            {
            });
            this._mapper.Setup(x => x.Map<RequestEntity>(It.IsAny<Request>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
            });

            this._requestRepo.Setup(s => s.UpdateEntity(It.IsAny<RequestEntity>()));
            this._requestRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<RequestEntity, bool>>>(), It.IsAny<Func<IQueryable<RequestEntity>, IIncludableQueryable<RequestEntity, object>>>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
                Maintenance = new List<MaintenanceEntity>(),
                Repairment = new List<RepairmentEntity>()
            });
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
            this._vehicleRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.AddRequest(tempRequestDTO, 1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void GetAllRequestsTest()
        {
            //Arrange

            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");

            this._mapper.Setup(x => x.Map<Request>(It.IsAny<RequestDTO>())).Returns(new Request(DateTime.Parse("2021-12-25T13:20:56.337Z"), DateTime.Parse("2021-12-26T13:20:56.337Z"), "test", Overall.RequestType.Fuelcard)
            {
            });
            this._mapper.Setup(x => x.Map<RequestEntity>(It.IsAny<Request>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
            });

            this._requestRepo.Setup(s => s.UpdateEntity(It.IsAny<RequestEntity>()));

            this._chauffeurRepo.Setup(s => s.GetAll(null));
            this._vehicleRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.GetAllRequests();

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAllRequestsPagingTest()
        {
            //Arrange

            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");

            this._mapper.Setup(x => x.Map<Request>(It.IsAny<RequestDTO>())).Returns(new Request(DateTime.Parse("2021-12-25T13:20:56.337Z"), DateTime.Parse("2021-12-26T13:20:56.337Z"), "test", Overall.RequestType.Fuelcard)
            {
            });
            this._mapper.Setup(x => x.Map<RequestEntity>(It.IsAny<Request>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
            });

            this._requestRepo.Setup(s => s.UpdateEntity(It.IsAny<RequestEntity>()));

            this._chauffeurRepo.Setup(s => s.GetAll(null));
            this._vehicleRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.GetAllRequestsPaging(new Overall.paging.GenericParameter());

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetRequestByIdTest()
        {
            //Arrange

            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");

            this._mapper.Setup(x => x.Map<Request>(It.IsAny<RequestDTO>())).Returns(new Request(DateTime.Parse("2021-12-25T13:20:56.337Z"), DateTime.Parse("2021-12-26T13:20:56.337Z"), "test", Overall.RequestType.Fuelcard)
            {
            });
            this._mapper.Setup(x => x.Map<RequestEntity>(It.IsAny<Request>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
            });

            this._requestRepo.Setup(s => s.UpdateEntity(It.IsAny<RequestEntity>()));
            this._requestRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<RequestEntity, bool>>>(), It.IsAny<Func<IQueryable<RequestEntity>, IIncludableQueryable<RequestEntity, object>>>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
                Maintenance = new List<MaintenanceEntity>(),
                Repairment = new List<RepairmentEntity>()
            });
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
            this._vehicleRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.GetRequestById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetRequestChaffeurTest()
        {
            //Arrange

            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");

            this._mapper.Setup(x => x.Map<Request>(It.IsAny<RequestDTO>())).Returns(new Request(DateTime.Parse("2021-12-25T13:20:56.337Z"), DateTime.Parse("2021-12-26T13:20:56.337Z"), "test", Overall.RequestType.Fuelcard)
            {
            });
            this._mapper.Setup(x => x.Map<RequestEntity>(It.IsAny<Request>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
            });

            this._requestRepo.Setup(s => s.UpdateEntity(It.IsAny<RequestEntity>()));
            this._requestRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<RequestEntity, bool>>>(), It.IsAny<Func<IQueryable<RequestEntity>, IIncludableQueryable<RequestEntity, object>>>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
                Maintenance = new List<MaintenanceEntity>(),
                Repairment = new List<RepairmentEntity>()
            });
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
            this._vehicleRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.GetRequestChaffeur(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetRequestVehicleTest()
        {
            //Arrange

            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");

            this._mapper.Setup(x => x.Map<Request>(It.IsAny<RequestDTO>())).Returns(new Request(DateTime.Parse("2021-12-25T13:20:56.337Z"), DateTime.Parse("2021-12-26T13:20:56.337Z"), "test", Overall.RequestType.Fuelcard)
            {
            });
            this._mapper.Setup(x => x.Map<RequestEntity>(It.IsAny<Request>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
            });

            this._requestRepo.Setup(s => s.UpdateEntity(It.IsAny<RequestEntity>()));
            this._requestRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<RequestEntity, bool>>>(), It.IsAny<Func<IQueryable<RequestEntity>, IIncludableQueryable<RequestEntity, object>>>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
                Maintenance = new List<MaintenanceEntity>(),
                Repairment = new List<RepairmentEntity>()
            });
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
            this._vehicleRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.GetRequestVehicle(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void GetRequestRepairsTest()
        {
            //Arrange

            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");

            this._mapper.Setup(x => x.Map<Request>(It.IsAny<RequestDTO>())).Returns(new Request(DateTime.Parse("2021-12-25T13:20:56.337Z"), DateTime.Parse("2021-12-26T13:20:56.337Z"), "test", Overall.RequestType.Fuelcard)
            {
            });
            this._mapper.Setup(x => x.Map<RequestEntity>(It.IsAny<Request>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
            });

            this._requestRepo.Setup(s => s.UpdateEntity(It.IsAny<RequestEntity>()));
            this._requestRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<RequestEntity, bool>>>(), It.IsAny<Func<IQueryable<RequestEntity>, IIncludableQueryable<RequestEntity, object>>>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
                Maintenance = new List<MaintenanceEntity>(),
                Repairment = new List<RepairmentEntity>()
            });
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
            this._vehicleRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.GetRequestRepairs(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetRequestMaintenanceTest()
        {
            //Arrange

            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");

            this._mapper.Setup(x => x.Map<Request>(It.IsAny<RequestDTO>())).Returns(new Request(DateTime.Parse("2021-12-25T13:20:56.337Z"), DateTime.Parse("2021-12-26T13:20:56.337Z"), "test", Overall.RequestType.Fuelcard)
            {
            });
            this._mapper.Setup(x => x.Map<RequestEntity>(It.IsAny<Request>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
            });

            this._requestRepo.Setup(s => s.UpdateEntity(It.IsAny<RequestEntity>()));
            this._requestRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<RequestEntity, bool>>>(), It.IsAny<Func<IQueryable<RequestEntity>, IIncludableQueryable<RequestEntity, object>>>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
                Maintenance = new List<MaintenanceEntity>(),
                Repairment = new List<RepairmentEntity>()
            });
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
            this._vehicleRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.GetRequestMaintenance(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void UpdateRequestTest()
        {
            //Arrange

            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");

            this._mapper.Setup(x => x.Map<Request>(It.IsAny<RequestDTO>())).Returns(new Request(DateTime.Parse("2021-12-25T13:20:56.337Z"), DateTime.Parse("2021-12-26T13:20:56.337Z"), "test", Overall.RequestType.Fuelcard)
            {
            });
            this._mapper.Setup(x => x.Map<RequestEntity>(It.IsAny<Request>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
            });

            this._requestRepo.Setup(s => s.UpdateEntity(It.IsAny<RequestEntity>()));
            this._requestRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<RequestEntity, bool>>>(), It.IsAny<Func<IQueryable<RequestEntity>, IIncludableQueryable<RequestEntity, object>>>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
                Maintenance = new List<MaintenanceEntity>(),
                Repairment = new List<RepairmentEntity>()
            });
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
            this._vehicleRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.UpdateRequest(tempRequestDTO,1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
