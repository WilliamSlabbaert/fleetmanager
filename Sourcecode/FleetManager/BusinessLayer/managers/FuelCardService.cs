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
        private readonly IValidator<FuelCardChaffeur> _validatorfcch;
        public List<GenericResponse> _errors { get; set; }
        public FuelCardService(IGenericRepo<FuelCardEntity> repo, IMapper mapper, IGenericRepo<ChaffeurEntity> chrepo, IValidator<FuelCard> _validator, IValidator<FuelCardChaffeur> validatorfcch)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._chrepo = chrepo;
            this._validator = _validator;
            this._validatorfcch = validatorfcch;
            _errors = new List<GenericResponse>();
        }

        public FuelCard AddFuelCard(FuelCard fc)
        {
            var results = _validator.Validate(fc);
            var temp = _mapper.Map<FuelCardEntity>(fc);
            if (results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                _repo.AddEntity(temp);
                _repo.Save();
            }
            return _mapper.Map<FuelCard>(temp);
        }
        public bool CheckExistingFuelCard(FuelCard fc)
        {
            var temp = _repo.GetAll(null).FirstOrDefault(s => s.CardNumber == fc.CardNumber);
            if(temp == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddFuelCardToChaffeur(int fuelcardNr, int chaffeurNr)
        {
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurNr);
            FuelCardEntity fc = GetFuelCardEntity(fuelcardNr);
            var tempch = _mapper.Map<Chaffeur>(ch);
            
            if (tempch.CheckFuelCard(fc.Id))
            {
                var results = _validatorfcch.Validate(new FuelCardChaffeur(_mapper.Map<Chaffeur>(ch), _mapper.Map<FuelCard>(fc),true));
                if(results.IsValid == false)
                {
                    _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
                }
                else
                {
                    ch.ChaffeurFuelCards.Add(new ChaffeurEntityFuelCardEntity(ch, fc, true));
                    _repo.Save();
                }
            }
            else
            {
                throw new Exception("Fuelcard already in chaffeur list.");
            }
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
