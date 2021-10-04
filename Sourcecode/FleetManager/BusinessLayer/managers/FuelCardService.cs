using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
        public List<GenericResponse> _errors { get; set; }
        public FuelCardService(IGenericRepo<FuelCardEntity> repo, IMapper mapper, IGenericRepo<ChaffeurEntity> chrepo, IValidator<FuelCard> _validator, IValidator<FuelCardChaffeur> validatorfcch, IValidator<FuelType> ft, IValidator<ExtraService> validatores)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._chrepo = chrepo;
            this._validator = _validator;
            this._validatorfcch = validatorfcch;
            this._validatorft = ft;
            this._validatores = validatores;
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
            var result = _validatorft.Validate(type);
            var fc = GetFuelCardEntity(fuelcardId);
            if (result.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(result.Errors);
            }
            else
            {
                fc.FuelType.Add(_mapper.Map<FuelEntity>(type));
                _repo.UpdateEntity(fc);
                _repo.Save();
            }
            return _mapper.Map<FuelCard>(fc);
        }
        public bool CheckExistingFuelCard(FuelCard fc)
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
            var result = _validatores.Validate(extraService);
            var fc = GetFuelCardEntity(fuelcardId);
            var extraS = _mapper.Map<ExtraServiceEntity>(extraService);
            if (result.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(result.Errors);
            }
            else
            {
                fc.Services.Add(extraS);
                _repo.UpdateEntity(fc);
                _repo.Save();
            }
            return _mapper.Map<FuelCard>(fc);
        }

        public Chaffeur AddFuelCardToChaffeur(int fuelcardNr, int chaffeurNr)
        {
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurNr);
            FuelCardEntity fc = GetFuelCardEntity(fuelcardNr);
            var tempch = _mapper.Map<Chaffeur>(ch);
            // var fuelcardCh = new FuelCardChaffeur(_mapper.Map<Chaffeur>(ch), _mapper.Map<FuelCard>(fc), true)
            if (tempch.CheckFuelCard(fuelcardNr))
            {
                foreach(var card in ch.ChaffeurFuelCards)
                {
                    card.IsActive = false;
                }
                ch.ChaffeurFuelCards.Add(new ChaffeurEntityFuelCardEntity(ch, fc, true));
                _chrepo.UpdateEntity(ch);
                _chrepo.Save();
            }
            else
            {
                throw new Exception("Fuelcard already in chaffeur list.");
            }
            return _mapper.Map<Chaffeur>(ch);
        }
        public Chaffeur UpdateChaffeurFuelCard(int fuelcardNr, int chaffeurNr,bool isactive)
        {
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurNr);
            if(isactive == true)
            {
                foreach (var card in ch.ChaffeurFuelCards)
                {
                    card.IsActive = false;
                }
            }
            var fuelcard = ch.ChaffeurFuelCards.FirstOrDefault(s => s.FuelCard.Id == fuelcardNr);
            fuelcard.IsActive = isactive;
            _chrepo.UpdateEntity(ch);
            _chrepo.Save();
            return _mapper.Map<Chaffeur>(ch);

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
        public List<FuelCard> GetAllFuelCards()
        {
            return _mapper.Map<List<FuelCard>>(this._repo.GetAll(
                x => x.Include(s => s.Services)
                .Include(s => s.FuelType)
                .Include(s => s.AuthenticationTypes)
                .Include(s => s.ChaffeurFuelCards)));
        }

        public FuelCard GetFuelCardById(int id)
        {
            return _mapper.Map<FuelCard>(_repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Services)
                .Include(s => s.FuelType)
                .Include(s => s.AuthenticationTypes)
                .Include(s => s.ChaffeurFuelCards)
                .ThenInclude(s => s.Chaffeur)));
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
        public void UpdateFuelCard(FuelCard fc)
        {
            this._repo.UpdateEntity(_mapper.Map<FuelCardEntity>(fc));
        }
    }
}
