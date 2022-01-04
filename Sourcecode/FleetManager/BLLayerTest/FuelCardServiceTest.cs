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
    public class FuelCardServiceTest
    {
        private Mock<IValidator<AuthenticationType>> _authenticationTypeValidator;
        private Mock<IGenericRepo<FuelCardEntity>> _fuelcardRepo;
        private Mock<IGenericRepo<ChauffeurEntity>> _chauffeurRepo;
        private Mock<IMapper> _mapper;
        private Mock<IMediator> _mediator;
        private Mock<IValidator<FuelCard>>_fuelcardValidator;
        private Mock<IValidator<FuelType>> _fueltypeValidator;
        private Mock<IValidator<ExtraService>> _serviceValidator;

        private FuelCardService _service;
        private GenericResult<GeneralModels> response = new GenericResult<GeneralModels>();

        FuelCardDTO tempFuelCardDTO = new FuelCardDTO()
        {
            CardNumber = "string",
            Pin = "1234",
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

        public FuelCardServiceTest()
        {
            response.Message = "OK";
            response.SetStatusCode(Overall.ResponseType.OK);

            this._chauffeurRepo = new Mock<IGenericRepo<ChauffeurEntity>>();
            this._fuelcardRepo = new Mock<IGenericRepo<FuelCardEntity>>();
            this._authenticationTypeValidator = new Mock<IValidator<AuthenticationType>>();
            this._mapper = new Mock<IMapper>();
            this._mediator = new Mock<IMediator>();
            this._fuelcardValidator = new Mock<IValidator<FuelCard>>();
            this._fueltypeValidator = new Mock<IValidator<FuelType>>();


            this._service = new FuelCardService(this._fuelcardRepo.Object, this._mapper.Object, this._chauffeurRepo.Object, this._mediator.Object, new FuelCardValidator(), new FuelTypeValidator(),new ExtraServiceValidator(),new AuthenticationValidator());
        }
        [Fact]
        public void AddFuelCardTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<FuelCard>(It.IsAny<FuelCardDTO>())).Returns(new FuelCard() {
                CardNumber = "2323",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true
            });

            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true
            });
            this._fuelcardRepo.Setup(s => s.AddEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.AddFuelCard(tempFuelCardDTO);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void AddFuelTypeTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<FuelType>(It.IsAny<FuelTypeDTO>())).Returns(new FuelType(fuel: Overall.FuelTypes.LPG) { 
                
            });

            this._mapper.Setup(x => x.Map<FuelCard>(It.IsAny<FuelCardEntity>())).Returns(new FuelCard()
            {
                CardNumber = "2323",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
            });


            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

            this._fuelcardRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<FuelCardEntity, bool>>>(), It.IsAny<Func<IQueryable<FuelCardEntity>, IIncludableQueryable<FuelCardEntity, object>>>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });
            this._fuelcardRepo.Setup(s => s.UpdateEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.AddFuelType(1,tempFuelTypeDTO);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void DeleteFuelTypeTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<FuelType>(It.IsAny<FuelTypeDTO>())).Returns(new FuelType(fuel: Overall.FuelTypes.LPG)
            {

            });

            this._mapper.Setup(x => x.Map<FuelCard>(It.IsAny<FuelCardEntity>())).Returns(new FuelCard()
            {
                CardNumber = "2323",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
            });


            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

            this._fuelcardRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<FuelCardEntity, bool>>>(), It.IsAny<Func<IQueryable<FuelCardEntity>, IIncludableQueryable<FuelCardEntity, object>>>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });
            this._fuelcardRepo.Setup(s => s.UpdateEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.DeleteFuelType(1, 2);

            //Assert
            Assert.NotNull(result);
            Assert.NotEqual("Ok", result.Message);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void AddServiceTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<ExtraService>(It.IsAny<ExtraServiceDTO>())).Returns(new ExtraService(Overall.ExtraServices.DiscountTires)
            {

            });
            this._mapper.Setup(x => x.Map<ExtraServiceEntity>(It.IsAny<ExtraService>())).Returns(new ExtraServiceEntity()
            {
                Service = Overall.ExtraServices.DiscountCarwash

            });
            this._mapper.Setup(x => x.Map<FuelCard>(It.IsAny<FuelCardEntity>())).Returns(new FuelCard()
            {
                CardNumber = "2323",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
            });


            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

            this._fuelcardRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<FuelCardEntity, bool>>>(), It.IsAny<Func<IQueryable<FuelCardEntity>, IIncludableQueryable<FuelCardEntity, object>>>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });
            this._fuelcardRepo.Setup(s => s.UpdateEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.AddService(tempServiceDTO, 2);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void AddAuthenticationTest()
        {
            //Arrange

            this._mapper.Setup(x => x.Map<AuthenticationType>(It.IsAny<AuthenticationTypeDTO>())).Returns(new AuthenticationType(Overall.AuthenticationTypes.PINKM)
            {

            });
            this._mapper.Setup(x => x.Map<AuthenticationTypeEntity>(It.IsAny<AuthenticationType>())).Returns(new AuthenticationTypeEntity()
            {
                type = Overall.AuthenticationTypes.PIN
            });
            this._mapper.Setup(x => x.Map<FuelCard>(It.IsAny<FuelCardEntity>())).Returns(new FuelCard()
            {
                CardNumber = "2323",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
            });


            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

            this._fuelcardRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<FuelCardEntity, bool>>>(), It.IsAny<Func<IQueryable<FuelCardEntity>, IIncludableQueryable<FuelCardEntity, object>>>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });
            this._fuelcardRepo.Setup(s => s.UpdateEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.AddAuthentication(tempAuthenticationTypeDTO, 2);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void AddFuelCardToChauffeurTest()
        {
            //Arrange


            this._mapper.Setup(x => x.Map<FuelCard>(It.IsAny<FuelCardEntity>())).Returns(new FuelCard()
            {
                CardNumber = "2323",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
            });

            this._mapper.Setup(x => x.Map<AuthenticationType>(It.IsAny<AuthenticationTypeDTO>())).Returns(new AuthenticationType(Overall.AuthenticationTypes.PIN)
            {
                type = Overall.AuthenticationTypes.PIN
            });


            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

            this._fuelcardRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<FuelCardEntity, bool>>>(), It.IsAny<Func<IQueryable<FuelCardEntity>, IIncludableQueryable<FuelCardEntity, object>>>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

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
               ,
                ChauffeurFuelCards = new List<FuelCardChauffeur>()
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
               ,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>()
            });
            this._fuelcardRepo.Setup(s => s.UpdateEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.AddFuelCardToChauffeur(1, 2);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void ActivityChauffeurFuelCardTest()
        {
            //Arrange


            this._mapper.Setup(x => x.Map<FuelCard>(It.IsAny<FuelCardEntity>())).Returns(new FuelCard()
            {
                CardNumber = "2323",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
            });

            this._mapper.Setup(x => x.Map<AuthenticationType>(It.IsAny<AuthenticationTypeDTO>())).Returns(new AuthenticationType(Overall.AuthenticationTypes.PIN)
            {
                type = Overall.AuthenticationTypes.PIN
            });


            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

            this._fuelcardRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<FuelCardEntity, bool>>>(), It.IsAny<Func<IQueryable<FuelCardEntity>, IIncludableQueryable<FuelCardEntity, object>>>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

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
               ,
                ChauffeurFuelCards = new List<FuelCardChauffeur>()
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
               ,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>()
            });
            this._fuelcardRepo.Setup(s => s.UpdateEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.ActivityChauffeurFuelCard(1, 2,true);

            //Assert
            Assert.NotNull(result);
            Assert.NotEqual("Ok", result.Message);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void UpdateFuelCardTest()
        {
            //Arrange


            this._mapper.Setup(x => x.Map<FuelCard>(It.IsAny<FuelCardDTO>())).Returns(new FuelCard()
            {
                CardNumber = "2323",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
            });

            this._mapper.Setup(x => x.Map<AuthenticationType>(It.IsAny<AuthenticationTypeDTO>())).Returns(new AuthenticationType(Overall.AuthenticationTypes.PIN)
            {
                type = Overall.AuthenticationTypes.PIN
            });


            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

            this._fuelcardRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<FuelCardEntity, bool>>>(), It.IsAny<Func<IQueryable<FuelCardEntity>, IIncludableQueryable<FuelCardEntity, object>>>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

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
               ,
                ChauffeurFuelCards = new List<FuelCardChauffeur>()
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
               ,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>()
            });
            this._fuelcardRepo.Setup(s => s.UpdateEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.UpdateFuelCard(1, tempFuelCardDTO);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ok", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void DeleteServiceTest()
        {
            //Arrange


            this._mapper.Setup(x => x.Map<FuelCard>(It.IsAny<FuelCardDTO>())).Returns(new FuelCard()
            {
                CardNumber = "2323",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
            });

            this._mapper.Setup(x => x.Map<AuthenticationType>(It.IsAny<AuthenticationTypeDTO>())).Returns(new AuthenticationType(Overall.AuthenticationTypes.PIN)
            {
                type = Overall.AuthenticationTypes.PIN
            });


            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

            this._fuelcardRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<FuelCardEntity, bool>>>(), It.IsAny<Func<IQueryable<FuelCardEntity>, IIncludableQueryable<FuelCardEntity, object>>>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

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
               ,
                ChauffeurFuelCards = new List<FuelCardChauffeur>()
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
               ,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>()
            });
            this._fuelcardRepo.Setup(s => s.UpdateEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.DeleteService(1, 2);

            //Assert
            Assert.NotNull(result);
            Assert.NotEqual("Ok", result.Message);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void DeleteAuthenticationTest()
        {
            //Arrange


            this._mapper.Setup(x => x.Map<FuelCard>(It.IsAny<FuelCardDTO>())).Returns(new FuelCard()
            {
                CardNumber = "2323",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
            });

            this._mapper.Setup(x => x.Map<AuthenticationType>(It.IsAny<AuthenticationTypeDTO>())).Returns(new AuthenticationType(Overall.AuthenticationTypes.PIN)
            {
                type = Overall.AuthenticationTypes.PIN
            });


            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

            this._fuelcardRepo.Setup(s => s.GetById(It.IsAny<Expression<Func<FuelCardEntity, bool>>>(), It.IsAny<Func<IQueryable<FuelCardEntity>, IIncludableQueryable<FuelCardEntity, object>>>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

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
               ,
                ChauffeurFuelCards = new List<FuelCardChauffeur>()
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
               ,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>()
            });
            this._fuelcardRepo.Setup(s => s.UpdateEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.DeleteAuthentication(1, 2);

            //Assert
            Assert.NotNull(result);
            Assert.NotEqual("Ok", result.Message);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void GetAllFuelCardsTest()
        {
            //Arrange


            this._mapper.Setup(x => x.Map<List<FuelCard>>(It.IsAny<List<FuelCardDTO>>()));
            CreateGenericResultCommand command = new CreateGenericResultCommand("OK", Overall.ResponseType.OK, null);
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");



            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });

          
            this._fuelcardRepo.Setup(s => s.UpdateEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.GetAllFuelCards();

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAllFuelCardsPagingTest()
        {
            //Arrange


            this._mapper.Setup(x => x.Map<List<FuelCard>>(It.IsAny<List<FuelCardDTO>>()));
            CreateGenericResultCommand command = new CreateGenericResultCommand("OK", Overall.ResponseType.OK, null);
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");



            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });


            this._fuelcardRepo.Setup(s => s.UpdateEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.GetAllFuelCardsPaging(new Overall.paging.GenericParameter());

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetFuelCardByIdTest()
        {
            //Arrange


            this._mapper.Setup(x => x.Map<List<FuelCard>>(It.IsAny<List<FuelCardDTO>>()));
            CreateGenericResultCommand command = new CreateGenericResultCommand("OK", Overall.ResponseType.OK, null);
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");



            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });


            this._fuelcardRepo.Setup(s => s.UpdateEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.GetFuelCardById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetFuelcardChauffeursTest()
        {
            //Arrange


            this._mapper.Setup(x => x.Map<List<FuelCard>>(It.IsAny<List<FuelCardDTO>>()));
            CreateGenericResultCommand command = new CreateGenericResultCommand("OK", Overall.ResponseType.OK, null);
            this._mediator.Setup(s => s.Send(It.IsAny<CreateGenericResultCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(this.response)
                .Verifiable("Notification was not sent.");



            this._mapper.Setup(x => x.Map<FuelCardEntity>(It.IsAny<FuelCard>())).Returns(new FuelCardEntity()
            {
                CardNumber = "424",
                Pin = "1234",
                ValidityDate = DateTime.Parse("2021-12-24T14:10:56.684Z"),
                IsActive = true,
                ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>(),
                FuelType = new List<FuelEntity>()
            });


            this._fuelcardRepo.Setup(s => s.UpdateEntity(It.IsAny<FuelCardEntity>()));
            this._fuelcardRepo.Setup(s => s.GetAll(null));
            this._fuelcardRepo.Setup(s => s.Save());

            ////Act
            var result = this._service.GetFuelcardChauffeurs(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", result.Message);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
