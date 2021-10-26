using AutoMapper;
using BusinessLayer.services.interfaces;
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

namespace BusinessLayer.services
{
    public class ChauffeurService : IChauffeurService
    {
        private readonly IGenericRepo<ChauffeurEntity> _repo;
        private readonly IGenericRepo<VehicleEntity> _vhrepo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        private IValidator<Chauffeur> _chauffeurValidator;
        public ChauffeurService(IGenericRepo<ChauffeurEntity> repo, IMapper mapper, IGenericRepo<VehicleEntity> vhrepo, IMediator mediator,
            ChauffeurValidator chauffeurV)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._vhrepo = vhrepo;
            this._mediator = mediator;
            this._chauffeurValidator = chauffeurV;
        }

        public GenericResult<GeneralModels> AddChauffeur(ChauffeurDTO dto)
        {
            var chauffeur = _mapper.Map<Chauffeur>(dto);
            var check = _chauffeurValidator.Validate(chauffeur);
            var result = GenericValidationCheck.CheckModel(check,"Chauffeur is invalid.");
            if (check.IsValid)
            {
                var chauffeurEntity = _mapper.Map<ChauffeurEntity>(chauffeur);
                var checkChauffeur = CheckExistingChauffeur(chauffeur, chauffeur.Id);
                var response = new GenericResult<GeneralModels>() { Message = "Chaffeur with same national insurence number already exists." };
                if (checkChauffeur == false)
                {
                    return response;
                }

                _repo.AddEntity(chauffeurEntity);
                _repo.Save();
                response.Message = "Ok";
                response.SetStatusCode(Overall.ResponseType.OK);
                response.ReturnValue = _mapper.Map<Chauffeur>(chauffeurEntity);
                return response;
            }
            return result;
        }
        public bool CheckExistingChauffeur(Chauffeur ch, int id)
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

        public GenericResult<GeneralModels> GetChauffeurById(int id)
        {
            var temp = _repo.GetById(
            filter: x => x.Id == id
            , x => x.Include(s => s.ChauffeurFuelCards)
            .ThenInclude(s => s.FuelCard)
            .Include(s => s.DrivingLicenses)
            .Include(s => s.Requests)
            .Include(s => s.ChauffeurVehicles)
            .ThenInclude(s => s.Vehicle));

            var value = temp == null ? null : _mapper.Map<Chauffeur>(temp);
            return  CreateResult(temp == null, value).Result;
        }

        public GenericResult<GeneralModels> UpdateChauffeur(ChauffeurDTO dto, int id)
        {
            var chauffeur = _mapper.Map<Chauffeur>(dto);
            var check = _chauffeurValidator.Validate(chauffeur);
            var result = GenericValidationCheck.CheckModel(check, "Chauffeur is invalid.");
            if (check.IsValid)
            {
                var checkChauffeur = CheckExistingChauffeur(chauffeur, id);
                var response = new GenericResult<GeneralModels>() { Message = "Chaffeur with same national insurence number already exists." };
                if (checkChauffeur == false)
                {
                    return response;
                }
                var temp = GetChauffeurEntity(id);

                temp.FirstName = dto.FirstName;
                temp.LastName = dto.LastName;

                temp.IsActive = dto.IsActive;
                temp.City = dto.City;

                temp.Street = dto.Street;
                temp.HouseNumber = dto.HouseNumber;

                temp.DateOfBirth = dto.DateOfBirth;
                temp.NationalInsurenceNumber = dto.NationalInsurenceNumber;

                _repo.UpdateEntity(temp);
                _repo.Save();
                response.SetStatusCode(Overall.ResponseType.OK);
                response.Message = "Ok";
                response.ReturnValue = _mapper.Map<Chauffeur>(temp);
                return response;
            }
            return result;
        }
        public GenericResult<GeneralModels> AddVehicleToChauffeur(int chaffeurNr, int vehicleNr)
        {
            VehicleEntity vh = GetVehicleEntity(vehicleNr);
            ChauffeurEntity ch = GetChauffeurEntity(chaffeurNr);
            var result = new GenericResult<GeneralModels>() { Message = "Vehicle is already in Chaffeurs list." };

            var chmodel = _mapper.Map<Chauffeur>(ch);
            if (chmodel.CheckVehicle(vh.Id))
            {
                var vhch = new ChauffeurEntityVehicleEntity(vh, ch, false);
                ch.ChauffeurVehicles.Add(vhch);
                _repo.UpdateEntity(ch);
                _repo.Save();
                result.SetStatusCode(Overall.ResponseType.OK);
                result.Message = "Ok";
                result.ReturnValue = _mapper.Map<Chauffeur>(ch);
                return result;
            }
            else
            {
                result.SetStatusCode(Overall.ResponseType.BadRequest);
                return result;
            }
        }
        public GenericResult<GeneralModels> UpdateVehicleToChauffeur(int chaffeurNr, int vehicleNr, bool active)
        {
            VehicleEntity vh = GetVehicleEntity(vehicleNr);
            ChauffeurEntity ch = GetChauffeurEntity(chaffeurNr);
            var result = new GenericResult<GeneralModels>() { Message = "Vehicle is already in Chaffeurs list." };

            var chmodel = _mapper.Map<Chauffeur>(ch);
            if (chmodel.CheckVehicle(vh.Id) == false)
            {
                var temp = ch.ChauffeurVehicles.FirstOrDefault(s => s.Vehicle.Id == vehicleNr);
                if (active == true)
                {
                    foreach (var chvh in ch.ChauffeurVehicles)
                    {
                        chvh.IsActive = false;
                    }
                }
                temp.IsActive = active;
                _repo.UpdateEntity(ch);
                _repo.Save();
                result.SetStatusCode(Overall.ResponseType.OK);
                result.Message = "Ok";
                result.ReturnValue = _mapper.Map<Chauffeur>(ch);
                return result;
            }
            else
            {
                result.SetStatusCode(Overall.ResponseType.BadRequest);
                return result;
            }
        }

        public ChauffeurEntity GetChauffeurEntity(int id)
        {
            var ch = _repo.GetById(
            filter: x => x.Id == id
            , x => x.Include(s => s.ChauffeurFuelCards)
            .ThenInclude(s => s.FuelCard)
            .Include(s => s.ChauffeurVehicles)
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
            .Include(s => s.ChauffeurVehicles)
            .Include(s => s.LicensePlates)
            .Include(s => s.Requests));
            return vh;
        }
        public GenericResult<GeneralModels> GetAllChauffeurs()
        {
            var temp = this._repo.GetAll(
                x => x.Include(s => s.ChauffeurFuelCards)
                .ThenInclude(s => s.FuelCard)
                .Include(s => s.ChauffeurVehicles)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.Requests));

            var value = _mapper.Map<List<Chauffeur>>(temp);
            return  CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetAllChauffeursPaging(GenericParameter parameters)
        {
            var temp = this._repo.GetAllWithPaging(
                x => x.Include(s => s.ChauffeurFuelCards)
                .ThenInclude(s => s.FuelCard)
                .Include(s => s.ChauffeurVehicles)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.Requests), parameters);

            var value = _mapper.Map<List<Chauffeur>>(temp);
            return  CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetChauffeurVehicles(int chaffeurId)
        {
            var temp = GetChauffeurEntity(chaffeurId);
            var value = temp == null ? null : _mapper.Map<Chauffeur>(temp).ChaffeurVehicles;
            return  CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetChauffeurRequests(int chaffeurId)
        {
            var temp = GetChauffeurEntity(chaffeurId);
            var value = temp == null ? null : _mapper.Map<Chauffeur>(temp).Requests;
            return  CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetChauffeurFuelcards(int chaffeurId)
        {
            var temp = GetChauffeurEntity(chaffeurId);
            var value = temp == null ? null : _mapper.Map<Chauffeur>(temp).ChaffeurFuelCards;
            return CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetChauffeurDrivingLicenses(int chaffeurId)
        {
            var temp = GetChauffeurEntity(chaffeurId);
            var value = temp == null ? null : _mapper.Map<Chauffeur>(temp).DrivingLicenses;
            return CreateResult(temp == null, value).Result;
        }
        public async Task<GenericResult<GeneralModels>> CreateResult(bool check, object value)
        {
            var message = "OK";
            var code = Overall.ResponseType.OK;
            if (check)
            {
                message = "Chaffeur('s) not found";
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
