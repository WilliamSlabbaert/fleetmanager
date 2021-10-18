using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.models.input;
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
        private IMediator _mediator;
        public ChaffeurService(IGenericRepo<ChaffeurEntity> repo, IMapper mapper, IGenericRepo<VehicleEntity> vhrepo, IMediator mediator)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._vhrepo = vhrepo;
            this._mediator = mediator;
        }

        public GenericResult<IGeneralModels> AddChaffeur(ChaffeurDTO ch)
        {
            var chaff = _mapper.Map<Chaffeur>(ch);
            var temp = _mapper.Map<ChaffeurEntity>(chaff);
            var check = CheckExistingChaffeur(chaff, chaff.Id);
            var result = new GenericResult<IGeneralModels>() { Message = "Chaffeur with same national insurence number already exists." };
            if (check == false)
            {
                return result;
            }

            _repo.AddEntity(temp);
            _repo.Save();
            result.Message = "Ok";
            result.SetStatusCode(Overall.ResponseType.OK);
            result.ReturnValue = _mapper.Map<Chaffeur>(temp);
            return result;
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

        public GenericResult<IGeneralModels> GetChaffeurById(int id)
        {
            var temp = _repo.GetById(
            filter: x => x.Id == id
            , x => x.Include(s => s.ChaffeurFuelCards)
            .ThenInclude(s => s.FuelCard)
            .Include(s => s.DrivingLicenses)
            .Include(s => s.Requests)
            .Include(s => s.ChaffeurVehicles)
            .ThenInclude(s => s.Vehicle));

            var value = temp == null ? null : _mapper.Map<Chaffeur>(temp);
            return CreateResult(temp == null, value);
        }

        public GenericResult<IGeneralModels> UpdateChaffeur(ChaffeurDTO ch, int id)
        {
            var chaff = _mapper.Map<Chaffeur>(ch);
            var check = CheckExistingChaffeur(chaff, id);
            var result = new GenericResult<IGeneralModels>() { Message = "Chaffeur with same national insurence number already exists." };
            if (check == false)
            {
                return result;
            }
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
            result.SetStatusCode(Overall.ResponseType.OK);
            result.Message = "Ok";
            result.ReturnValue = _mapper.Map<Chaffeur>(temp);
            return result;
        }
        public GenericResult<IGeneralModels> AddVehicleToChaffeur(int chaffeurNr, int vehicleNr)
        {
            VehicleEntity vh = GetVehicleEntity(vehicleNr);
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurNr);
            var result = new GenericResult<IGeneralModels>() { Message = "Vehicle is already in Chaffeurs list." };

            var chmodel = _mapper.Map<Chaffeur>(ch);
            if (chmodel.CheckVehicle(vh.Id))
            {
                var vhch = new ChaffeurEntityVehicleEntity(vh, ch, false);
                ch.ChaffeurVehicles.Add(vhch);
                _repo.UpdateEntity(ch);
                _repo.Save();
                result.SetStatusCode(Overall.ResponseType.OK);
                result.Message = "Ok";
                result.ReturnValue = _mapper.Map<Chaffeur>(ch);
                return result;
            }
            else
            {
                result.SetStatusCode(Overall.ResponseType.BadRequest);
                return result;
            }
        }
        public GenericResult<IGeneralModels> UpdateVehicleToChaffeur(int chaffeurNr, int vehicleNr, bool active)
        {
            VehicleEntity vh = GetVehicleEntity(vehicleNr);
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurNr);
            var result = new GenericResult<IGeneralModels>() { Message = "Vehicle is already in Chaffeurs list." };

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
                result.SetStatusCode(Overall.ResponseType.OK);
                result.Message = "Ok";
                result.ReturnValue = _mapper.Map<Chaffeur>(ch);
                return result;
            }
            else
            {
                result.SetStatusCode(Overall.ResponseType.BadRequest);
                return result;
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
        public GenericResult<IGeneralModels> GetAllChaffeurs()
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
        public GenericResult<IGeneralModels> GetAllChaffeursPaging(GenericParameter parameters)
        {
            var temp = this._repo.GetAllWithPaging(
                x => x.Include(s => s.ChaffeurFuelCards)
                .ThenInclude(s => s.FuelCard)
                .Include(s => s.ChaffeurVehicles)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.Requests), parameters);

            var value = temp;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetChaffeurVehicles(int chaffeurId)
        {
            var temp = GetChaffeurEntity(chaffeurId);
            var value = temp == null ? null : _mapper.Map<Chaffeur>(temp).ChaffeurVehicles;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetChaffeurRequests(int chaffeurId)
        {
            var temp = GetChaffeurEntity(chaffeurId);
            var value = temp == null ? null : _mapper.Map<Chaffeur>(temp).Requests;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetChaffeurFuelcards(int chaffeurId)
        {
            var temp = GetChaffeurEntity(chaffeurId);
            var value = temp == null ? null : _mapper.Map<Chaffeur>(temp).ChaffeurFuelCards;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetChaffeurDrivingLicenses(int chaffeurId)
        {
            var temp = GetChaffeurEntity(chaffeurId);
            var value = temp == null ? null : _mapper.Map<Chaffeur>(temp).DrivingLicenses;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> CreateResult(bool check, object value)
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
