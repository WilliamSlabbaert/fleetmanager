using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers
{
    public class FuelCardService : IFuelCardService
    {
        private readonly IGenericRepo<FuelCardEntity> _repo;
        private readonly IGenericRepo<ChaffeurEntity> _chrepo;
        private readonly IMapper _mapper;
        private readonly IValidator<FuelCard> _validator;
        private readonly IValidator<FuelType> _validatorft;
        private readonly IValidator<ExtraService> _validatores;
        private readonly IValidator<FuelCardChaffeur> _validatorfcch;
        private readonly IValidator<AuthenticationType> _validatorat;
        private IMediator _mediator;
        public List<GenericResponse> _errors { get; set; }
        public FuelCardService(IGenericRepo<FuelCardEntity> repo, IMapper mapper, IGenericRepo<ChaffeurEntity> chrepo, IValidator<FuelCard> _validator, IValidator<FuelCardChaffeur> validatorfcch, IValidator<FuelType> ft, IValidator<ExtraService> validatores, IValidator<AuthenticationType> validatorat,IMediator mediator)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._chrepo = chrepo;
            this._validator = _validator;
            this._validatorfcch = validatorfcch;
            this._validatorft = ft;
            this._validatores = validatores;
            this._validatorat = validatorat;
            this._mediator = mediator;
            _errors = new List<GenericResponse>();
        }

        public FuelCard AddFuelCard(FuelCard fc)
        {
            var temp = _mapper.Map<FuelCardEntity>(fc);
            _repo.AddEntity(temp);
            _repo.Save();
            return _mapper.Map<FuelCard>(temp);
        }
        public FuelCard AddFuelType(int fuelcardId, FuelType type)
        {
            var fc = GetFuelCardEntity(fuelcardId);

            fc.FuelType.Add(_mapper.Map<FuelEntity>(type));
            _repo.UpdateEntity(fc);
            _repo.Save();
            return _mapper.Map<FuelCard>(fc);
        }
        public bool CheckExistingFuelCard(FuelCard fc)
        {
            if (fc.Id != 0)
            {
                var tempList = _repo.GetAll(null).Where(s => s.Id != fc.Id);
                var temp = tempList.FirstOrDefault(s => s.CardNumber == fc.CardNumber);
                if (temp == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                var temp = _repo.GetAll(null).FirstOrDefault(s => s.CardNumber == fc.CardNumber);
                if (temp == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public FuelCard DeleteFuelType(int id, int fuelid)
        {
            var result = GetFuelCardEntity(id);

            var dl = result.FuelType.FirstOrDefault(s => s.Id == fuelid);
            result.FuelType.Remove(dl);

            _repo.UpdateEntity(result);
            _repo.Save();

            return _mapper.Map<FuelCard>(result);
        }
        public FuelCard AddService(ExtraService extraService, int fuelcardId)
        {
            var fc = GetFuelCardEntity(fuelcardId);
            var extraS = _mapper.Map<ExtraServiceEntity>(extraService);


            fc.Services.Add(extraS);
            _repo.UpdateEntity(fc);
            _repo.Save();
            return _mapper.Map<FuelCard>(fc);
        }
        public bool CheckValidationService(ExtraService extraService)
        {
            var result = _validatores.Validate(extraService);
            if (result.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(result.Errors);
                return false;
            }
            return true;
        }
        public bool CheckValidationFuelType(FuelType fueltype)
        {
            var result = _validatorft.Validate(fueltype);
            if (result.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(result.Errors);
                return false;
            }
            return true;
        }
        public bool CheckValidationAuthentication(AuthenticationType authenticationType)
        {
            var result = _validatorat.Validate(authenticationType);
            if (result.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(result.Errors);
                return false;
            }
            return true;
        }
        public FuelCard AddAuthentication(AuthenticationType authenticationType, int fuelcardId)
        {
            var fuelcard = GetFuelCardEntity(fuelcardId);
            var aT = _mapper.Map<AuthenticationTypeEntity>(authenticationType);
            fuelcard.AuthenticationTypes.Add(aT);
            _repo.UpdateEntity(fuelcard);
            _repo.Save();

            return _mapper.Map<FuelCard>(fuelcard);
        }

        public GenericResult<IGeneralModels> AddFuelCardToChaffeur(int fuelcardNr, int chaffeurNr)
        {
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurNr);
            FuelCardEntity fc = GetFuelCardEntity(fuelcardNr);
            var result = new GenericResult<IGeneralModels>() { Message = "Fuelcard already exist's in chaffeurs list." };
            var tempch = _mapper.Map<Chaffeur>(ch);
            if (tempch.CheckFuelCard(fuelcardNr))
            {
                ch.ChaffeurFuelCards.Add(new ChaffeurEntityFuelCardEntity(ch, fc, false));
                _chrepo.UpdateEntity(ch);
                _chrepo.Save();
                result.SetStatusCode(Overall.ResponseType.OK);
                result.Message = "Ok";
                result.ReturnValue = ch;
                return result;
            }
            return result;
        }
        public GenericResult<IGeneralModels> ActivityChaffeurFuelCard(int fuelcardNr, int chaffeurNr, bool isactive)
        {
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurNr);
            var result = new GenericResult<IGeneralModels>() { Message = "Fuelcard doesn't in chaffeurs list." };
            if (isactive == true)
            {
                foreach (var card in ch.ChaffeurFuelCards)
                {
                    card.IsActive = false;
                }
            }
            var fuelcard = ch.ChaffeurFuelCards.FirstOrDefault(s => s.FuelCard.Id == fuelcardNr);
            if(fuelcard == null)
            {
                return result;
            }
            fuelcard.IsActive = isactive;
            _chrepo.UpdateEntity(ch);
            _chrepo.Save();
            result.SetStatusCode(Overall.ResponseType.OK);
            result.Message = "Ok";
            result.ReturnValue = _mapper.Map<Chaffeur>(ch);
            return result;

        }
        public bool CheckValidationFuelCardChaffeur(FuelCardChaffeur fuelCardChaffeur)
        {
            var results = _validatorfcch.Validate(fuelCardChaffeur);
            if (results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
                return false;
            }
            return true;
        }
        public bool CheckValidationFuelCard(FuelCard fuelcard)
        {
            var results = _validator.Validate(fuelcard);
            if (results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
                return false;
            }
            return true;
        }
        public FuelCard DeleteService(int id, int fuelcardId)
        {
            var temp = GetFuelCardEntity(fuelcardId);
            var obj = temp.Services.FirstOrDefault(s => s.Id == id);

            temp.Services.Remove(obj);
            _repo.UpdateEntity(temp);
            _repo.Save();

            return _mapper.Map<FuelCard>(temp);

        }
        public GenericResult<IGeneralModels> GetAllFuelCards()
        {
            var temp = _mapper.Map<List<FuelCard>>(this._repo.GetAll(
                x => x.Include(s => s.Services)
                .Include(s => s.FuelType)
                .Include(s => s.AuthenticationTypes)
                .Include(s => s.ChaffeurFuelCards)));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetAllFuelCardsPaging(GenericParameter parameters)
        {
            var temp = _mapper.Map<List<FuelCard>>(this._repo.GetAllWithPaging(
                x => x.Include(s => s.Services)
                .Include(s => s.FuelType)
                .Include(s => s.AuthenticationTypes)
                .Include(s => s.ChaffeurFuelCards),parameters));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value);
        }

        public GenericResult<IGeneralModels> GetFuelCardById(int id)
        {
            var temp = _mapper.Map<FuelCard>(GetFuelCardEntity(id));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value);
        }
        
        public GenericResult<IGeneralModels> GetFuelcardCHaffeurs(int id)
        {
            var temp = GetFuelCardEntity(id);

            var value = temp == null ? null : temp.ChaffeurFuelCards;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetFuelcardFuelTypes(int id)
        {
            var temp = GetFuelCardEntity(id);

            var value = temp == null ? null : temp.FuelType;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetFuelcardAuthenications(int id)
        {
            var temp = GetFuelCardEntity(id);

            var value = temp == null ? null : temp.AuthenticationTypes;
            return CreateResult(temp == null, value);
        }
        public FuelCardEntity GetFuelCardEntity(int id)
        {
            return _repo.GetById(
            filter: x => x.Id == id,
            x => x.Include(s => s.Services)
            .Include(s => s.FuelType)
            .Include(s => s.ChaffeurFuelCards)
            .ThenInclude(s => s.Chaffeur));
        }
        public ChaffeurEntity GetChaffeurEntity(int id)
        {
            var ch = _chrepo.GetById(
            filter: x => x.Id == id
            , x => x.Include(s => s.ChaffeurFuelCards)
            .Include(s => s.ChaffeurVehicles)
            .Include(s => s.DrivingLicenses)
            .Include(s => s.Requests));
            return ch;
        }
        public FuelCard UpdateFuelCard(FuelCard fuelcard, int fuelcardId)
        {
            var fc = GetFuelCardEntity(fuelcardId);
            fc.CardNumber = fuelcard.CardNumber;
            fc.IsActive = fuelcard.IsActive;
            fc.Pin = fuelcard.Pin;
            fc.ValidityDate = fuelcard.ValidityDate;

            _repo.UpdateEntity(fc);
            _repo.Save();
            return _mapper.Map<FuelCard>(fc);
        }
        public GenericResult<IGeneralModels> CreateResult(bool check, object value)
        {
            var message = "OK";
            var code = Overall.ResponseType.OK;
            if (check)
            {
                message = "Fuelcard('s) not found";
                code = Overall.ResponseType.NotFound;
                value = null;
            }
            var resp = _mediator.Send(new CreateGenericResultCommand(message, code, value));
            return resp.Result;
        }
        public object GetHeaders(GenericParameter parameters)
        {
            var temp = _repo.GetAll(null);
            var temp2 = _mediator.Send(new GetHeadersQuery(parameters, temp)).Result;
            var metadata = new
            {
                temp2.TotalCount,
                temp2.PageSize,
                temp2.CurrentPage,
                temp2.HasNext,
                temp2.HasPrevious
            };
            return metadata;
        }
    }
}
