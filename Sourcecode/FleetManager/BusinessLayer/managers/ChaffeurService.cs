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

        public void AddChaffeur(Chaffeur ch)
        {
            var results = _validator.Validate(ch);
            if (results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                var temp  = _repo.GetAll(null);
                if(temp.FirstOrDefault(s => s.NationalInsurenceNumber == ch.NationalInsurenceNumber) == null)
                {
                    _repo.AddEntity(_mapper.Map<ChaffeurEntity>(ch));
                    _repo.Save();
                }
                else
                {
                    throw new Exception("Chaffeur already exists.");
                }
            }
        }

        public Chaffeur GetChaffeurById(int id)
        {
            return _mapper.Map<Chaffeur>(_repo.GetById(
            filter: x => x.Id == id
            , x => x.Include(s => s.ChaffeurFuelCards)
            .ThenInclude(s => s.FuelCard)
            .Include(s => s.DrivingLicenses)
            .Include(s => s.Requests)
            .Include(s => s.ChaffeurVehicles)
            .ThenInclude(s => s.Vehicle)));
        }

        public void UpdateChaffeur(Chaffeur ch)
        {
            var results = _validator.Validate(ch);
            if (results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                _repo.UpdateEntity(_mapper.Map<ChaffeurEntity>(ch));
                _repo.Save();
            }
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
                    ch.ChaffeurVehicles.Add(new ChaffeurEntityVehicleEntity(vh,ch,true));
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
            try
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
            catch
            {
                throw new Exception("Chaffeur is null.");
            }
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
            return _mapper.Map<List<Chaffeur>>(this._repo.GetAll(
                x => x.Include(s => s.ChaffeurFuelCards)
                .Include(s => s.ChaffeurVehicles)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.Requests)));
        }
    }
}
