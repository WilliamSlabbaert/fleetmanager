using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.mediator.commands;
using BusinessLayer.models;
using BusinessLayer.validators;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using FluentValidation.Results;
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
    public class ChaffeurService : IChaffeurService
    {
        private readonly IGenericRepo<ChaffeurEntity> _repo;
        private readonly IGenericRepo<VehicleEntity> _vhrepo;
        private readonly IMapper _mapper;
        private readonly IValidator<Chaffeur> _validator;
        private readonly IValidator<VehicleChaffeur> _validatorvhch;
        private IMediator _mediator;
        public List<GenericResponse> _errors { get; set; }
        public ChaffeurService(IGenericRepo<ChaffeurEntity> repo, IMapper mapper, IGenericRepo<VehicleEntity> vhrepo, IValidator<Chaffeur> val, IValidator<VehicleChaffeur> validatorvhch, IMediator mediator)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._vhrepo = vhrepo;
            this._validator = val;
            this._validatorvhch = validatorvhch;
            this._mediator = mediator;
            this._errors = new List<GenericResponse>();
        }

        public Chaffeur AddChaffeur(Chaffeur ch)
        {
            var temp = _mapper.Map<ChaffeurEntity>(ch);
            var check = CheckExistingChaffeur(ch, ch.Id);
            if (check == false)
            {
                throw new Exception("Chaffeur with same national insurence number already exists.");
            }
            _repo.AddEntity(temp);
            _repo.Save();
            return _mapper.Map<Chaffeur>(temp);
        }
        public bool CheckExistingChaffeur(Chaffeur ch, int id)
        {
            var result = _repo.GetAll(null).FirstOrDefault(s => s.NationalInsurenceNumber == ch.NationalInsurenceNumber && s.Id != id);
            if (result == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public GenericResult GetChaffeurById(int id)
        {
            var temp = _repo.GetById(
            filter: x => x.Id == id
            , x => x.Include(s => s.ChaffeurFuelCards)
            .ThenInclude(s => s.FuelCard)
            .Include(s => s.DrivingLicenses)
            .Include(s => s.Requests)
            .Include(s => s.ChaffeurVehicles)
            .ThenInclude(s => s.Vehicle));

            var resp = new GenericResult();
            if (temp == null)
            {
                resp.Message = "Chaffeur not found.";
                resp.SetStatusCode(Overall.ResponseType.NotFound);
                return resp;
            }
            resp.Message = "Ok";
            resp.SetStatusCode(Overall.ResponseType.OK);
            resp.ReturnValue = _mapper.Map<Chaffeur>(temp);
            return resp;
        }
        public FuelCard GetFuelcardFromChaffeur(Chaffeur chaffeur, int fuelcardId)
        {
            var result = chaffeur.ChaffeurFuelCards.FirstOrDefault(s => s.FuelCard.Id == fuelcardId);
            if (result == null)
            {
                throw new Exception("FuelCard not found in chaffeurs list.");
            }
            return result.FuelCard;
        }

        public Chaffeur UpdateChaffeur(Chaffeur ch, int id)
        {
            var temp = GetChaffeurEntity(id);

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
            return _mapper.Map<Chaffeur>(temp);
        }
        public Chaffeur AddVehicleToChaffeur(int chaffeurNr, int vehicleNr)
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
                    foreach (var chvh in ch.ChaffeurVehicles)
                    {
                        chvh.IsActive = false;
                    }
                    ch.ChaffeurVehicles.Add(new ChaffeurEntityVehicleEntity(vh, ch, true));
                    _repo.UpdateEntity(ch);
                    _repo.Save();
                }
            }
            else
            {
                throw new Exception("Vehicle is already in Chaffeurs list.");
            }
            return _mapper.Map<Chaffeur>(ch);
        }
        public bool CheckValidationChaffeur(Chaffeur chaffeur)
        {
            var results = _validator.Validate(chaffeur);
            if (results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
                return false;
            }
            return true;
        }
        public Chaffeur UpdateVehicleToChaffeur(int chaffeurNr, int vehicleNr, bool active)
        {
            VehicleEntity vh = GetVehicleEntity(vehicleNr);
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurNr);

            var chmodel = _mapper.Map<Chaffeur>(ch);
            if (chmodel.CheckVehicle(vh.Id) == false)
            {
                var temp = ch.ChaffeurVehicles.FirstOrDefault(s => s.Vehicle.Id == vehicleNr);
                if (active == true)
                {
                    foreach (var chvh in ch.ChaffeurVehicles)
                    {
                        chvh.IsActive = false;
                    }
                }
                temp.IsActive = active;
                _repo.UpdateEntity(ch);
                _repo.Save();
            }
            else
            {
                throw new Exception("Vehicle doesn't exist in Chaffeurs list.");
            }
            return _mapper.Map<Chaffeur>(ch);
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
        public GenericResult GetAllChaffeurs()
        {
            var temp = this._repo.GetAll(
                x => x.Include(s => s.ChaffeurFuelCards)
                .ThenInclude(s => s.FuelCard)
                .Include(s => s.ChaffeurVehicles)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.Requests));

            var value = _mapper.Map<List<Chaffeur>>(temp);
            return CreateResult(temp == null, value);
        }
        public GenericResult GetAllChaffeursPaging(GenericParameter parameters)
        {
            var temp = this._repo.GetAllWithPaging(
                x => x.Include(s => s.ChaffeurFuelCards)
                .ThenInclude(s => s.FuelCard)
                .Include(s => s.ChaffeurVehicles)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.Requests),parameters);

            var value = _mapper.Map<List<Chaffeur>>(temp);
            return CreateResult(temp == null, value);
        }
        public GenericResult GetChaffeurVehicles(int chaffeurId)
        {
            var temp = GetChaffeurEntity(chaffeurId);
            var value = temp == null ? null : _mapper.Map<Chaffeur>(temp).ChaffeurVehicles;
            return CreateResult(temp == null, value);
        }
        public GenericResult GetChaffeurRequests(int chaffeurId)
        {
            var temp = GetChaffeurEntity(chaffeurId);
            var value = temp == null ? null : _mapper.Map<Chaffeur>(temp).Requests;
            return CreateResult(temp == null, value);
        }
        public GenericResult GetChaffeurFuelcards(int chaffeurId)
        {
            var temp = GetChaffeurEntity(chaffeurId);
            var value = temp == null ? null : _mapper.Map<Chaffeur>(temp).ChaffeurFuelCards;
            return CreateResult(temp == null, value);
        }
        public GenericResult GetChaffeurDrivingLicenses(int chaffeurId)
        {
            var temp = GetChaffeurEntity(chaffeurId);
            var value = temp == null ? null : _mapper.Map<Chaffeur>(temp).DrivingLicenses;
            return CreateResult(temp == null, value);
        }
        public GenericResult CreateResult(bool check, object value)
        {
            var message = "OK";
            var code = Overall.ResponseType.OK;
            if (check)
            {
                message = "Chaffeur('s) not found";
                code = Overall.ResponseType.NotFound;
                value = null;
            }
            var resp = _mediator.Send(new CreateGenericResultCommand(message, code, value));
            return resp.Result;
        }
    }
}
