using AutoMapper;
using BusinessLayer.services.interfaces;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.models.input;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Overall.paging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.validators;
using FluentValidation;

namespace BusinessLayer.services
{
    public class FuelCardService : IFuelCardService
    {
        private readonly IGenericRepo<FuelCardEntity> _repo;
        private readonly IGenericRepo<ChauffeurEntity> _chrepo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        private IValidator<FuelCard> _fuelcardValidator;
        private IValidator<FuelType> _fueltypeValidator;
        private IValidator<ExtraService> _serviceValidator;
        private IValidator<AuthenticationType> _authenticationTypeValidator;
        public FuelCardService(IGenericRepo<FuelCardEntity> repo, IMapper mapper, IGenericRepo<ChauffeurEntity> chrepo, IMediator mediator, 
            FuelCardValidator fuelcardV , 
            FuelTypeValidator fueltypeV, 
            ExtraServiceValidator serviceV,
            AuthenticationValidator authenticationV)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._chrepo = chrepo;
            this._mediator = mediator;
            this._fuelcardValidator = fuelcardV;
            this._fueltypeValidator = fueltypeV;
            this._serviceValidator = serviceV;
            this._authenticationTypeValidator = authenticationV;
        }

        public GenericResult<GeneralModels> AddFuelCard(FuelCardDTO dto)
        {
            var fc = _mapper.Map<FuelCard>(dto);
            var validate = _fuelcardValidator.Validate(fc);
            var check = GenericValidationCheck.CheckModel(validate,"Fuelcard not valid");
            if (validate.IsValid)
            {
                var temp = _mapper.Map<FuelCardEntity>(fc);
                var respond = new GenericResult<GeneralModels>() { Message = "Fuelcard with same cardnumber already exist's." };
                if (CheckExistingFuelcard(fc, fc.Id) == false)
                {
                    return respond;
                }
                _repo.AddEntity(temp);
                _repo.Save();
                respond = new GenericResult<GeneralModels>() { Message = "Ok", ReturnValue = _mapper.Map<FuelCard>(temp) };
                respond.SetStatusCode(Overall.ResponseType.OK);
                return respond;
            }
            return check;
        }
        public bool CheckExistingFuelcard(FuelCard ch, int id)
        {
            var result = _repo.GetAll(null).FirstOrDefault(s => s.CardNumber == ch.CardNumber && s.Id != id);
            if (result == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public GenericResult<GeneralModels> AddFuelType(int fuelcardId, FuelTypeDTO dto)
        {
            var fueltype = _mapper.Map<FuelType>(dto);
            var check = _fueltypeValidator.Validate(fueltype);
            var checkResult = GenericValidationCheck.CheckModel(check, "Fueltype is invalid.");
            if (check.IsValid)
            {
                var fuelcardEntity = GetFuelCardEntity(fuelcardId);
                var checkModel = _mapper.Map<FuelCard>(fuelcardEntity);
                var respond = new GenericResult<GeneralModels>() { Message = "Fuel type already exist in fuelcard list." };
                if (checkModel.CheckExistingFuelType(fueltype) == false)
                {
                    return respond;
                }
                fuelcardEntity.FuelType.Add(_mapper.Map<FuelEntity>(fueltype));
                _repo.UpdateEntity(fuelcardEntity);
                _repo.Save();
                respond = new GenericResult<GeneralModels>() { Message = "Ok", ReturnValue = _mapper.Map<FuelCard>(fuelcardEntity) };
                respond.SetStatusCode(Overall.ResponseType.OK);
                return respond;
            }
            return checkResult;
        }
        public GenericResult<GeneralModels> DeleteFuelType(int id, int fuelid)
        {
            var result = GetFuelCardEntity(id);
            var dl = result.FuelType.FirstOrDefault(s => s.Id == fuelid);
            var respond = new GenericResult<GeneralModels>() { Message = "Fuel type doesn't exist in fuelcard list." };

            if (dl == null)
            {
                return respond;
            }

            result.FuelType.Remove(dl);
            _repo.UpdateEntity(result);
            _repo.Save();
            respond.Message = "Ok";
            respond.ReturnValue = _mapper.Map<FuelCard>(result);
            respond.SetStatusCode(Overall.ResponseType.OK);
            return respond;
        }
        public GenericResult<GeneralModels> AddService(ExtraServiceDTO dto, int fuelcardId)
        {
            var service = _mapper.Map<ExtraService>(dto);
            var check = _serviceValidator.Validate(service);
            var checkResult = GenericValidationCheck.CheckModel(check, "Extra service is invalid");
            if(check.IsValid)
            {
                var fuelcardEntity = GetFuelCardEntity(fuelcardId);
                var extraS = _mapper.Map<ExtraServiceEntity>(service);
                var checkModel = _mapper.Map<FuelCard>(fuelcardEntity);
                var respond = new GenericResult<GeneralModels>() { Message = "Extra service already exist in fuelcard list." };
                if (checkModel.CheckExistingServices(service) == false)
                {
                    return respond;
                }
                fuelcardEntity.Services.Add(extraS);
                _repo.UpdateEntity(fuelcardEntity);
                _repo.Save();

                respond.Message = "Ok";
                respond.ReturnValue = _mapper.Map<FuelCard>(fuelcardEntity);
                respond.SetStatusCode(Overall.ResponseType.OK);
                return respond;
            }
            return checkResult;
        }
        public GenericResult<GeneralModels> AddAuthentication(AuthenticationTypeDTO dto, int fuelcardId)
        {
            var authenticationType = _mapper.Map<AuthenticationType>(dto);
            var check = _authenticationTypeValidator.Validate(authenticationType);
            var checkResult = GenericValidationCheck.CheckModel(check, "Authentication type is invalid");
            if (check.IsValid)
            {
                var fuelcard = GetFuelCardEntity(fuelcardId);
                var aT = _mapper.Map<AuthenticationTypeEntity>(authenticationType);
                var respond = new GenericResult<GeneralModels>() { Message = "Authentication type already exist in fuelcard list." };

                var temp = _mapper.Map<FuelCard>(fuelcard);
                if (temp.CheckExistingAuthentications(authenticationType) == false)
                {
                    return respond;
                }
                fuelcard.AuthenticationTypes.Add(aT);
                _repo.UpdateEntity(fuelcard);
                _repo.Save();
                respond.Message = "Ok";
                respond.ReturnValue = _mapper.Map<FuelCard>(fuelcard);
                respond.SetStatusCode(Overall.ResponseType.OK);

                return respond;
            }
            return checkResult;
        }

        public GenericResult<GeneralModels> AddFuelCardToChauffeur(int fuelcardNr, int chaffeurNr)
        {
            ChauffeurEntity ch = GetChauffeurEntity(chaffeurNr);
            FuelCardEntity fc = GetFuelCardEntity(fuelcardNr);
            var result = new GenericResult<GeneralModels>() { Message = "Fuelcard already exist's in chaffeurs list." };
            var tempch = _mapper.Map<Chauffeur>(ch);
            if (tempch.CheckFuelCard(fuelcardNr))
            {
                ch.ChauffeurFuelCards.Add(new ChauffeurEntityFuelCardEntity(ch, fc, false));
                _chrepo.UpdateEntity(ch);
                _chrepo.Save();
                result.SetStatusCode(Overall.ResponseType.OK);
                result.Message = "Ok";
                result.ReturnValue = ch;
                return result;
            }
            return result;
        }
        public GenericResult<GeneralModels> ActivityChauffeurFuelCard(int fuelcardNr, int chaffeurNr, bool isactive)
        {
            ChauffeurEntity ch = GetChauffeurEntity(chaffeurNr);
            var result = new GenericResult<GeneralModels>() { Message = "Fuelcard doesn't in chaffeurs list." };
            if (isactive == true)
            {
                foreach (var card in ch.ChauffeurFuelCards)
                {
                    card.IsActive = false;
                }
            }
            var fuelcard = ch.ChauffeurFuelCards.FirstOrDefault(s => s.FuelCard.Id == fuelcardNr);
            if (fuelcard == null)
            {
                return result;
            }
            fuelcard.IsActive = isactive;
            _chrepo.UpdateEntity(ch);
            _chrepo.Save();
            result.SetStatusCode(Overall.ResponseType.OK);
            result.Message = "Ok";
            result.ReturnValue = _mapper.Map<Chauffeur>(ch);
            return result;

        }
        public GenericResult<GeneralModels> UpdateFuelCard(int fuelcardNr, FuelCardDTO dto)
        {
            var fuelcard = _mapper.Map<FuelCard>(dto);
            var validate = _fuelcardValidator.Validate(fuelcard);
            var check = GenericValidationCheck.CheckModel(validate, "Fuelcard not valid");
            if (validate.IsValid) {
                var fuelcardEntity = GetFuelCardEntity(fuelcardNr);
                var respond = new GenericResult<GeneralModels>() { Message = "Fuelcard with same cardnumber already exist's." };
                if (CheckExistingFuelcard(fuelcard, fuelcardNr) == false)
                {
                    return respond;
                }
                fuelcardEntity.CardNumber = fuelcard.CardNumber;
                fuelcardEntity.ValidityDate = fuelcard.ValidityDate;
                fuelcardEntity.Pin = fuelcard.Pin;
                fuelcardEntity.IsActive = fuelcard.IsActive;
                _repo.UpdateEntity(fuelcardEntity);
                _repo.Save();
                respond = new GenericResult<GeneralModels>() { ReturnValue = _mapper.Map<FuelCard>(fuelcardEntity), Message = "Ok" };
                respond.SetStatusCode(Overall.ResponseType.OK);

                return respond;
            }
            return check;
        }
        public GenericResult<GeneralModels> DeleteService(int fuelcardId, int serviceId)
        {
            var fuelcard = GetFuelCardEntity(fuelcardId);
            var respond = new GenericResult<GeneralModels>() { Message = "Extra service doesn't exist in fuelcard list." };
            var temp = fuelcard.Services.FirstOrDefault(s => s.Id == serviceId);
            if (temp == null)
            {
                return respond;
            }
            fuelcard.Services.Remove(temp);
            _repo.UpdateEntity(fuelcard);
            _repo.Save();
            respond = new GenericResult<GeneralModels>() { ReturnValue = _mapper.Map<FuelCard>(fuelcard), Message = "Ok" };
            respond.SetStatusCode(Overall.ResponseType.OK);

            return respond;

        }
        public GenericResult<GeneralModels> DeleteAuthentication(int fuelcardId, int authenticationId)
        {
            var fuelcard = GetFuelCardEntity(fuelcardId);
            var respond = new GenericResult<GeneralModels>() { Message = "Authentication type doesn't exist in fuelcard list." };
            var temp = fuelcard.AuthenticationTypes.FirstOrDefault(s => s.Id == authenticationId);
            if (temp == null)
            {
                return respond;
            }
            fuelcard.AuthenticationTypes.Remove(temp);
            _repo.UpdateEntity(fuelcard);
            _repo.Save();
            respond = new GenericResult<GeneralModels>() { ReturnValue = _mapper.Map<FuelCard>(fuelcard), Message = "Ok" };
            respond.SetStatusCode(Overall.ResponseType.OK);

            return respond;

        }
        public GenericResult<GeneralModels> GetAllFuelCards()
        {
            var temp = _mapper.Map<List<FuelCard>>(this._repo.GetAll(
                x => x.Include(s => s.Services)
                .Include(s => s.FuelType)
                .Include(s => s.AuthenticationTypes)
                .Include(s => s.ChauffeurFuelCards)));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetAllFuelCardsPaging(GenericParameter parameters)
        {
            var temp = _mapper.Map<List<FuelCard>>(this._repo.GetAllWithPaging(
                x => x.Include(s => s.Services)
                .Include(s => s.FuelType)
                .Include(s => s.AuthenticationTypes)
                .Include(s => s.ChauffeurFuelCards), parameters));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value).Result;
        }

        public GenericResult<GeneralModels> GetFuelCardById(int id)
        {
            var temp = _mapper.Map<FuelCard>(GetFuelCardEntity(id));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value).Result;
        }

        public GenericResult<GeneralModels> GetFuelcardChauffeurs(int id)
        {
            var temp = GetFuelCardEntity(id);

            var value = temp == null ? null : temp.ChauffeurFuelCards;
            return CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetFuelcardFuelTypes(int id)
        {
            var temp = GetFuelCardEntity(id);

            var value = temp == null ? null : temp.FuelType;
            return CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetFuelcardAuthenications(int id)
        {
            var temp = GetFuelCardEntity(id);

            var value = temp == null ? null : temp.AuthenticationTypes;
            return CreateResult(temp == null, value).Result;
        }
        public FuelCardEntity GetFuelCardEntity(int id)
        {
            return _repo.GetById(
            filter: x => x.Id == id,
            x =>
            x.Include(s => s.FuelType)
            .Include(s => s.ChauffeurFuelCards)
            .ThenInclude(s => s.Chauffeur)
            .Include(s => s.AuthenticationTypes)
            .Include(s => s.Services));
        }
        public ChauffeurEntity GetChauffeurEntity(int id)
        {
            var ch = _chrepo.GetById(
            filter: x => x.Id == id
            , x => x.Include(s => s.ChauffeurFuelCards)
            .Include(s => s.ChauffeurVehicles));
            return ch;
        }
        public async Task<GenericResult<GeneralModels>> CreateResult(bool check, object value)
        {
            var message = "OK";
            var code = Overall.ResponseType.OK;
            if (check)
            {
                message = "Fuelcard('s) not found";
                code = Overall.ResponseType.NotFound;
                value = null;
            }
            var resp = await _mediator.Send(new CreateGenericResultCommand(message, code, value));
            return resp;
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
