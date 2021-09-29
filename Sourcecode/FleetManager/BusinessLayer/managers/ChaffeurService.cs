using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using BusinessLayer.validators;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers
{
    public class ChaffeurService : IChaffeurService
    {
        private readonly IGenericRepo<ChaffeurEntity> _repo;
        private readonly IGenericRepo<VehicleEntity> _vhrepo;
        private readonly IMapper _mapper;
        private readonly IValidator<Chaffeur> _validator;
        private readonly IValidator<VehicleChaffeur> _validatorvhch;
        public List<GenericResponse> _errors { get; set; }
        public ChaffeurService(IGenericRepo<ChaffeurEntity> repo, IMapper mapper, IGenericRepo<VehicleEntity> vhrepo, IValidator<Chaffeur> val, IValidator<VehicleChaffeur> validatorvhch)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._vhrepo = vhrepo;
            this._validator = val;
            this._validatorvhch = validatorvhch;
            this._errors = new List<GenericResponse>();
        }

        public Chaffeur AddChaffeur(Chaffeur ch)
        {
            var results = _validator.Validate(ch);
            var temp = _mapper.Map<ChaffeurEntity>(ch);
            if (results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                _repo.AddEntity(temp);
                _repo.Save();
            }
            return _mapper.Map<Chaffeur>(temp);
        }
        public bool checkExistingChaffeur(Chaffeur ch)
        {
            if(ch.Id != 0){
                var temp = _repo.GetAll(null).Where(s => s.Id != ch.Id);
                var result = temp.FirstOrDefault(s => s.NationalInsurenceNumber == ch.NationalInsurenceNumber);
                if(result == null)
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
                var result1 = _repo.GetAll(null).FirstOrDefault(s => s.NationalInsurenceNumber == ch.NationalInsurenceNumber);
                if (result1 == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Chaffeur GetChaffeurById(int id)
        {
            var temp = _repo.GetById(
            filter: x => x.Id == id
            , x => x.Include(s => s.ChaffeurFuelCards)
            .ThenInclude(s => s.FuelCard)
            .Include(s => s.DrivingLicenses)
            .Include(s => s.Requests)
            .Include(s => s.ChaffeurVehicles)
            .ThenInclude(s => s.Vehicle));
            return _mapper.Map<Chaffeur>(temp);
        }

        public Chaffeur UpdateChaffeur(Chaffeur ch, int id)
        {
            var results = _validator.Validate(ch);
            var temp = GetChaffeurEntity(id);
            if (results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                temp.FirstName = ch.FirstName;
                temp.LastName = ch.LastName;

                temp.IsActive = ch.IsActive;
                temp.City = ch.City;

                temp.Street = ch.Street;
                temp.HouseNumber = ch.HouseNumber;

                temp.DateOfBirth = ch.DateOfBirth;
                temp.NationalInsurenceNumber = ch.NationalInsurenceNumber;

                _repo.UpdateEntity(temp);
                _repo.Save();
            }
            return _mapper.Map<Chaffeur>(temp);
        }
        public void AddVehicleToChaffeur(int chaffeurNr, int vehicleNr)
        {
            VehicleEntity vh = GetVehicleEntity(vehicleNr);
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurNr);

            var chmodel = _mapper.Map<Chaffeur>(ch);
            if (chmodel.CheckVehicle(vh.Id))
            {
                var vhch = new VehicleChaffeur(_mapper.Map<Vehicle>(vh), _mapper.Map<Chaffeur>(ch), true);
                var results = _validatorvhch.Validate(vhch);
                if (results.IsValid == false)
                {
                    _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
                }
                else
                {
                    ch.ChaffeurVehicles.Add(new ChaffeurEntityVehicleEntity(vh, ch, true));
                    _repo.Save();
                }
            }
            else
            {
                throw new Exception("Vehicle is already in Chaffeurs list.");
            }
        }

        public ChaffeurEntity GetChaffeurEntity(int id)
        {
            var ch = _repo.GetById(
            filter: x => x.Id == id
            , x => x.Include(s => s.ChaffeurFuelCards)
            .ThenInclude(s => s.FuelCard)
            .Include(s => s.ChaffeurVehicles)
            .ThenInclude(s => s.Vehicle)
            .Include(s => s.DrivingLicenses)
            .Include(s => s.Requests));
            return ch;
        }
        public VehicleEntity GetVehicleEntity(int id)
        {
            var vh = _vhrepo.GetById(
            filter: x => x.Id == id
            , x => x.Include(s => s.LicensePlates)
            .Include(s => s.ChaffeurVehicles)
            .Include(s => s.LicensePlates)
            .Include(s => s.Requests));
            return vh;
        }
        public List<Chaffeur> GetAllChaffeurs()
        {
            var temp = this._repo.GetAll(
                x => x.Include(s => s.ChaffeurFuelCards)
                .ThenInclude(s => s.FuelCard)
                .Include(s => s.ChaffeurVehicles)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.Requests));

            return _mapper.Map<List<Chaffeur>>(temp);
        }
    }
}
