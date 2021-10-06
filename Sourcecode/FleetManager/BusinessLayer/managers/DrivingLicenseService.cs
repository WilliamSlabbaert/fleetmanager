using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.mediator.commands;
using BusinessLayer.models;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers
{
    public class DrivingLicenseService : IDrivingLicenseService
    {
        private readonly IGenericRepo<DrivingLicenseEntity> _repo;
        private readonly IGenericRepo<ChaffeurEntity> _chrepo;
        private readonly IMapper _mapper;
        private readonly IValidator<DrivingLicense> _validator;
        private IMediator _mediator;
        public List<GenericResponse> _errors { get; set; }
        public DrivingLicenseService(IGenericRepo<DrivingLicenseEntity> repo, IMapper mapper, IGenericRepo<ChaffeurEntity> chrepo, IValidator<DrivingLicense> validator, IMediator mediator)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._chrepo = chrepo;
            this._validator = validator;
            this._mediator = mediator;
            _errors = new List<GenericResponse>();
        }
        public DrivingLicense AddDrivingLicense(DrivingLicense drivinglicense, int chaffeurid)
        {
            var ch = GetChaffeurEntity(chaffeurid);
            var dl = _mapper.Map<DrivingLicenseEntity>(drivinglicense);

            ch.DrivingLicenses.Add(dl);
            _chrepo.UpdateEntity(ch);
            _chrepo.Save();
            return _mapper.Map<DrivingLicense>(dl);
        }
        public bool CheckValidationDrivingLicense(DrivingLicense drivinglicense)
        {
            var results = _validator.Validate(drivinglicense);
            if (results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
                return false;
            }
            return true;
        }
        public Chaffeur DeleteDrivingLicense(int drivinglicense, int chaffeurid)
        {
            var temp = GetChaffeurEntity(chaffeurid);
            var temp2 = temp.DrivingLicenses.FirstOrDefault(s => s.Id == drivinglicense);
            if (temp2 != null)
            {
                temp.DrivingLicenses.Remove(temp2);
                _chrepo.UpdateEntity(temp);
                _chrepo.Save();
            }
            return _mapper.Map<Chaffeur>(temp);
        }
        public GenericResult GetAllDrivingLicenses()
        {
            var temp = _mapper.Map<List<DrivingLicense>>(_repo.GetAll(
                s => s.Include(x => x.Chaffeur)));
            var value = temp == null ? null : _mapper.Map<List<DrivingLicense>>(temp);
            return CreateResult(temp == null, value);
        }
        public GenericResult GetAllDrivingLicenseById(int id)
        {
            var value = _mapper.Map<DrivingLicense>(_repo.GetById(
                filter: x => x.Id == id,
                including: s => s.Include(x => x.Chaffeur)));

            return CreateResult(value == null, value);
        }
        public GenericResult GetDrivingLicenseChaffeurById(int id)
        {
            var temp = _mapper.Map<DrivingLicense>(_repo.GetById(
                filter: x => x.Id == id,
                including: s => s.Include(x => x.Chaffeur)));

            var value = temp == null ? null : temp.Chaffeur;
            return CreateResult(temp == null, value);
        }
        public ChaffeurEntity GetChaffeurEntity(int id)
        {
            var temp = _chrepo.GetById(
            filter: x => x.Id == id
            , x => x.Include(s => s.ChaffeurFuelCards)
            .ThenInclude(s => s.FuelCard)
            .Include(s => s.DrivingLicenses)
            .Include(s => s.Requests)
            .Include(s => s.ChaffeurVehicles)
            .ThenInclude(s => s.Vehicle));
            return temp;

        }
        public bool CheckExistingDrivingLicense(int id, DrivingLicense license)
        {
            var temp = _mapper.Map<Chaffeur>(GetChaffeurEntity(id));
            return temp.CheckDrivingLicense(license);
        }
        public GenericResult CreateResult(bool check, object value)
        {
            var message = "OK";
            var code = Overall.ResponseType.OK;
            if (check)
            {
                message = "Driving license('s) not found";
                code = Overall.ResponseType.NotFound;
                value = null;
            }
            var resp = _mediator.Send(new CreateGenericResultCommand(message, code, value));
            return resp.Result;
        }

    }
}
