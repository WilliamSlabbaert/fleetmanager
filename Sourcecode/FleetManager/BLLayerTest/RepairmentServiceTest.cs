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
    public class RepairmentServiceTest
    {
        private Mock<IGenericRepo<RequestEntity>> _requestRepo;
        private Mock<IGenericRepo<RepairmentEntity>> _repairmentRepo;
        private Mock<IMapper> _mapper;
        private Mock<IMediator> _mediator;
        private Mock<IValidator<Repairment>> _repairmentValidator;

        private RepairmentService _service;
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

        public RepairmentServiceTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);

            this._requestRepo = new Mock<IGenericRepo<RequestEntity>>();
            this._repairmentRepo = new Mock<IGenericRepo<RepairmentEntity>>();
            this._mapper = new Mock<IMapper>();
            this._mediator = new Mock<IMediator>();
            this._repairmentValidator = new Mock<IValidator<Repairment>>();

            this._service = new RepairmentService(this._requestRepo.Object,this._mapper.Object,this._repairmentRepo.Object,this._mediator.Object,new BusinessLayer.validators.RepairmentValidator());
        }
        [Fact]
        public void AddRepairmentTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Repairment>(It.IsAny<RepairmentDTO>())).Returns(new Repairment(DateTime.Parse("2021-12-24T14:37:07.592Z"),"test","test"));
            this._mapper.Setup(x => x.Map<RepairmentEntity>(It.IsAny<Repairment>())).Returns(new RepairmentEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Description = "test",
                Company = "Garage"
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
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());
            this._repairmentRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.AddRepairment(tempRepairmentDTO, 1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void DeleteRepairmentTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Repairment>(It.IsAny<RepairmentDTO>())).Returns(new Repairment(DateTime.Parse("2021-12-24T14:37:07.592Z"), "test", "test"));
            this._mapper.Setup(x => x.Map<RepairmentEntity>(It.IsAny<Repairment>())).Returns(new RepairmentEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Description = "test",
                Company = "Garage"
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
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());
            this._repairmentRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.DeleteRepairment(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAllRepairmentsTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Repairment>(It.IsAny<RepairmentDTO>())).Returns(new Repairment(DateTime.Parse("2021-12-24T14:37:07.592Z"), "test", "test"));

            this._mapper.Setup(x => x.Map<List<Repairment>>(It.IsAny<List<RepairmentEntity>>()));
            this._mapper.Setup(x => x.Map<RepairmentEntity>(It.IsAny<Repairment>())).Returns(new RepairmentEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Description = "test",
                Company = "Garage"
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
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());
            this._repairmentRepo.Setup(s => s.Save());
            this._repairmentRepo.Setup(s => s.GetAll(null));
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync(this.response)
             .Verifiable("Notification was not sent.");
            ////Act
            var result = this._service.GetAllRepairments();

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAllRepairmentsPagingTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Repairment>(It.IsAny<RepairmentDTO>())).Returns(new Repairment(DateTime.Parse("2021-12-24T14:37:07.592Z"), "test", "test"));

            this._mapper.Setup(x => x.Map<List<Repairment>>(It.IsAny<List<RepairmentEntity>>()));
            this._mapper.Setup(x => x.Map<RepairmentEntity>(It.IsAny<Repairment>())).Returns(new RepairmentEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Description = "test",
                Company = "Garage"
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
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());
            this._repairmentRepo.Setup(s => s.Save());
            this._repairmentRepo.Setup(s => s.GetAll(null));
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync(this.response)
             .Verifiable("Notification was not sent.");
            ////Act
            var result = this._service.GetAllRepairmentsPaging(new Overall.paging.GenericParameter());

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetRepairmentRequestByIdTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Repairment>(It.IsAny<RepairmentDTO>())).Returns(new Repairment(DateTime.Parse("2021-12-24T14:37:07.592Z"), "test", "test"));

            this._mapper.Setup(x => x.Map<List<Repairment>>(It.IsAny<List<RepairmentEntity>>()));
            this._mapper.Setup(x => x.Map<RepairmentEntity>(It.IsAny<Repairment>())).Returns(new RepairmentEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Description = "test",
                Company = "Garage"
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
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());
            this._repairmentRepo.Setup(s => s.Save());
            this._repairmentRepo.Setup(s => s.GetAll(null));
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync(this.response)
             .Verifiable("Notification was not sent.");
            ////Act
            var result = this._service.GetRepairmentRequestById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
