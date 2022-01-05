using AutoMapper;
using BusinessLayer;
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
    public class MaintenanceServiceTest
    {
        private Mock<IGenericRepo<RequestEntity>> _requestRepo;
        private Mock<IGenericRepo<MaintenanceEntity>> _maintenanceRepo;
        private Mock<IMapper> _mapper;
        private Mock<IMediator> _mediator;
        private Mock<IValidator<Maintenance>> _maintenanceValidator;
        private Mock<IValidator<Invoice>> _invoiceValidator;

        private MaintenanceService _service;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();

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
        RequestDTO tempRequestDTO = new RequestDTO()
        {
            StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
            EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
            Status = "test",
            Type = Overall.RequestType.Fuelcard
        };

        public MaintenanceServiceTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);

            this._requestRepo = new Mock<IGenericRepo<RequestEntity>>();
            this._maintenanceRepo = new Mock<IGenericRepo<MaintenanceEntity>>();
            this._mapper = new Mock<IMapper>();
            this._mediator = new Mock<IMediator>();
            this._maintenanceValidator = new Mock<IValidator<Maintenance>>();
            this._invoiceValidator = new Mock<IValidator<Invoice>>();

            this._service = new MaintenanceService(this._requestRepo.Object, this._mapper.Object, this._maintenanceRepo.Object, this._mediator.Object, new BusinessLayer.validators.MaintenanceValidator(), new BusinessLayer.validators.InvoiceValidator());
        }
        [Fact]
        public void AddMaintenanceTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Maintenance>(It.IsAny<MaintenanceDTO>())).Returns(new Maintenance(DateTime.Now, 123, "ok"));
            this._mapper.Setup(x => x.Map<MaintenanceEntity>(It.IsAny<Maintenance>())).Returns(new MaintenanceEntity() {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage"
            });
            this._requestRepo.Setup(s => s.UpdateEntity(It.IsAny<RequestEntity>()));
            this._requestRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<RequestEntity, bool>>>(), It.IsAny<Func<IQueryable<RequestEntity>, IIncludableQueryable<RequestEntity, object>>>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
                Maintenance = new List<MaintenanceEntity>()
            });
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());
            this._maintenanceRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.AddMaintenance(tempMaintenanceDTO, 1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void DeleteMaintenanceTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Maintenance>(It.IsAny<MaintenanceDTO>())).Returns(new Maintenance(DateTime.Now, 123, "ok"));
            this._mapper.Setup(x => x.Map<MaintenanceEntity>(It.IsAny<Maintenance>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage"
            });
            this._requestRepo.Setup(s => s.UpdateEntity(It.IsAny<RequestEntity>()));
            this._requestRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<RequestEntity, bool>>>(), It.IsAny<Func<IQueryable<RequestEntity>, IIncludableQueryable<RequestEntity, object>>>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
                Maintenance = new List<MaintenanceEntity>()
            });
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());
            this._maintenanceRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.DeleteMaintenance(1, 1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void UpdateMaintenanceTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Maintenance>(It.IsAny<MaintenanceDTO>())).Returns(new Maintenance(DateTime.Now, 123, "ok"));
            this._mapper.Setup(x => x.Map<MaintenanceEntity>(It.IsAny<Maintenance>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage"
            });
            this._requestRepo.Setup(s => s.UpdateEntity(It.IsAny<RequestEntity>()));
            this._requestRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<RequestEntity, bool>>>(), It.IsAny<Func<IQueryable<RequestEntity>, IIncludableQueryable<RequestEntity, object>>>())).Returns(new RequestEntity()
            {
                StartDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                EndDate = DateTime.Parse("2021-12-25T13:20:56.337Z"),
                Status = "test",
                Type = Overall.RequestType.Fuelcard,
                Maintenance = new List<MaintenanceEntity>()
            });
            this._requestRepo.Setup(s => s.GetAll(null));
            this._requestRepo.Setup(s => s.Save());
            this._maintenanceRepo.Setup(s => s.Save());
            this._maintenanceRepo.Setup(s => s.UpdateEntity(It.IsAny<MaintenanceEntity>()));
            this._maintenanceRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<MaintenanceEntity, bool>>>(), It.IsAny<Func<IQueryable<MaintenanceEntity>, IIncludableQueryable<MaintenanceEntity, object>>>())).Returns(new MaintenanceEntity() {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage"
            });

            ////Act
            var result = this._service.UpdateMaintenance(1, tempMaintenanceDTO);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAllMaintenancesTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Maintenance>(It.IsAny<MaintenanceDTO>())).Returns(new Maintenance(DateTime.Now, 123, "ok"));
            this._mapper.Setup(x => x.Map<MaintenanceEntity>(It.IsAny<Maintenance>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage"
            });

            this._mapper.Setup(x => x.Map<List<Maintenance>>(It.IsAny<List<MaintenanceEntity>>()));
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");
            this._requestRepo.Setup(s => s.GetAll(null));
            this._maintenanceRepo.Setup(s => s.GetAll(null));



            ////Act
            var result = this._service.GetAllMaintenances();

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAllMaintenancesPagingTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Maintenance>(It.IsAny<MaintenanceDTO>())).Returns(new Maintenance(DateTime.Now, 123, "ok"));
            this._mapper.Setup(x => x.Map<MaintenanceEntity>(It.IsAny<Maintenance>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage"
            });

            this._mapper.Setup(x => x.Map<List<Maintenance>>(It.IsAny<List<MaintenanceEntity>>()));
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");
            this._requestRepo.Setup(s => s.GetAll(null));
            this._maintenanceRepo.Setup(s => s.GetAll(null));

            this._maintenanceRepo.Setup(s => s.GetAllWithPaging(It.IsAny<Func<IQueryable<MaintenanceEntity>, IIncludableQueryable<MaintenanceEntity, object>>>(), It.IsAny<GenericParameter>()));



            ////Act
            var result = this._service.GetAllMaintenancesPaging(new Overall.paging.GenericParameter());

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetMaintenanceByIdTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Maintenance>(It.IsAny<MaintenanceDTO>())).Returns(new Maintenance(DateTime.Now, 123, "ok"));
            this._mapper.Setup(x => x.Map<MaintenanceEntity>(It.IsAny<Maintenance>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage"
            });

            this._mapper.Setup(x => x.Map<List<Maintenance>>(It.IsAny<List<MaintenanceEntity>>()));
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");
            this._requestRepo.Setup(s => s.GetAll(null));
            this._maintenanceRepo.Setup(s => s.GetAll(null));
            this._maintenanceRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<MaintenanceEntity, bool>>>(), It.IsAny<Func<IQueryable<MaintenanceEntity>, IIncludableQueryable<MaintenanceEntity, object>>>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage"
            });

            this._maintenanceRepo.Setup(s => s.GetAllWithPaging(It.IsAny<Func<IQueryable<MaintenanceEntity>, IIncludableQueryable<MaintenanceEntity, object>>>(), It.IsAny<GenericParameter>()));



            ////Act
            var result = this._service.GetMaintenanceById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetMaintenanceInvoicesByIdTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Maintenance>(It.IsAny<MaintenanceDTO>())).Returns(new Maintenance(DateTime.Now, 123, "ok"));
            this._mapper.Setup(x => x.Map<MaintenanceEntity>(It.IsAny<Maintenance>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage"
            });

            this._mapper.Setup(x => x.Map<List<Maintenance>>(It.IsAny<List<MaintenanceEntity>>()));
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");
            this._requestRepo.Setup(s => s.GetAll(null));
            this._maintenanceRepo.Setup(s => s.GetAll(null));
            this._maintenanceRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<MaintenanceEntity, bool>>>(), It.IsAny<Func<IQueryable<MaintenanceEntity>, IIncludableQueryable<MaintenanceEntity, object>>>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage"
            });

            this._maintenanceRepo.Setup(s => s.GetAllWithPaging(It.IsAny<Func<IQueryable<MaintenanceEntity>, IIncludableQueryable<MaintenanceEntity, object>>>(), It.IsAny<GenericParameter>()));



            ////Act
            var result = this._service.GetMaintenanceInvoicesById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetMaintenanceRequestByIdTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Maintenance>(It.IsAny<MaintenanceDTO>())).Returns(new Maintenance(DateTime.Now, 123, "ok"));
            this._mapper.Setup(x => x.Map<MaintenanceEntity>(It.IsAny<Maintenance>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage"
            });

            this._mapper.Setup(x => x.Map<List<Maintenance>>(It.IsAny<List<MaintenanceEntity>>()));
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");
            this._requestRepo.Setup(s => s.GetAll(null));
            this._maintenanceRepo.Setup(s => s.GetAll(null));
            this._maintenanceRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<MaintenanceEntity, bool>>>(), It.IsAny<Func<IQueryable<MaintenanceEntity>, IIncludableQueryable<MaintenanceEntity, object>>>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage"
            });

            this._maintenanceRepo.Setup(s => s.GetAllWithPaging(It.IsAny<Func<IQueryable<MaintenanceEntity>, IIncludableQueryable<MaintenanceEntity, object>>>(), It.IsAny<GenericParameter>()));



            ////Act
            var result = this._service.GetMaintenanceRequestById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void AddInvoiceTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Maintenance>(It.IsAny<MaintenanceDTO>())).Returns(new Maintenance(DateTime.Now, 123, "ok"));
            this._mapper.Setup(x => x.Map<MaintenanceEntity>(It.IsAny<Maintenance>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage",
                Invoices = new List<InvoiceEntity>()
            });

            this._mapper.Setup(x => x.Map<List<Maintenance>>(It.IsAny<List<MaintenanceEntity>>()));
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");
            this._requestRepo.Setup(s => s.GetAll(null));
            this._maintenanceRepo.Setup(s => s.GetAll(null));
            this._maintenanceRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<MaintenanceEntity, bool>>>(), It.IsAny<Func<IQueryable<MaintenanceEntity>, IIncludableQueryable<MaintenanceEntity, object>>>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage",
                Invoices = new List<InvoiceEntity>()
            });
            this._mapper.Setup(x => x.Map<Invoice>(It.IsAny<InvoiceDTO>())).Returns(new Invoice() { InvoiceImage = "image" });
            this._mapper.Setup(x => x.Map<InvoiceEntity>(It.IsAny<Invoice>())).Returns(new InvoiceEntity() { InvoiceImage = "image" });
            this._maintenanceRepo.Setup(s => s.GetAllWithPaging(It.IsAny<Func<IQueryable<MaintenanceEntity>, IIncludableQueryable<MaintenanceEntity, object>>>(), It.IsAny<GenericParameter>()));



            ////Act
            var result = this._service.AddInvoice(1,this.tempInvoiceDTO);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void DeleteInvoiceTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<Maintenance>(It.IsAny<MaintenanceDTO>())).Returns(new Maintenance(DateTime.Now, 123, "ok"));
            this._mapper.Setup(x => x.Map<MaintenanceEntity>(It.IsAny<Maintenance>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage",
                Invoices = new List<InvoiceEntity>()
            });

            this._mapper.Setup(x => x.Map<List<Maintenance>>(It.IsAny<List<MaintenanceEntity>>()));
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(this.response)
               .Verifiable("Notification was not sent.");
            this._requestRepo.Setup(s => s.GetAll(null));
            this._maintenanceRepo.Setup(s => s.GetAll(null));
            this._maintenanceRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<MaintenanceEntity, bool>>>(), It.IsAny<Func<IQueryable<MaintenanceEntity>, IIncludableQueryable<MaintenanceEntity, object>>>())).Returns(new MaintenanceEntity()
            {
                Date = DateTime.Parse("2021-12-24T14:37:07.592Z"),
                Price = 10,
                Garage = "Garage",
                Invoices = new List<InvoiceEntity>()
            });
            this._mapper.Setup(x => x.Map<Invoice>(It.IsAny<InvoiceDTO>())).Returns(new Invoice() { InvoiceImage = "image" });
            this._mapper.Setup(x => x.Map<InvoiceEntity>(It.IsAny<Invoice>())).Returns(new InvoiceEntity() { InvoiceImage = "image" });
            this._maintenanceRepo.Setup(s => s.GetAllWithPaging(It.IsAny<Func<IQueryable<MaintenanceEntity>, IIncludableQueryable<MaintenanceEntity, object>>>(), It.IsAny<GenericParameter>()));



            ////Act
            var result = this._service.DeleteInvoice(1, 2);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Invoice doesn't exist in maintenance list.", result.Message);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
