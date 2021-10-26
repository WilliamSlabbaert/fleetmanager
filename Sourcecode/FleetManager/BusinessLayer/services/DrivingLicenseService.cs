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
using FluentValidation;
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
    public class DrivingLicenseService : IDrivingLicenseService
    {
        private readonly IGenericRepo<DrivingLicenseEntity> _repo;
        private readonly IGenericRepo<ChauffeurEntity> _chrepo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        public DrivingLicenseService(IGenericRepo<DrivingLicenseEntity> repo, IMapper mapper, IGenericRepo<ChauffeurEntity> chrepo, IMediator mediator)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._chrepo = chrepo;
            this._mediator = mediator;
        }
        public GenericResult<GeneralModels> AddDrivingLicense(DrivingLicenseDTO drivinglicense, int chaffeurid)
        {
            var temp = _mapper.Map<DrivingLicense>(drivinglicense);
            var ch = GetChauffeurEntity(chaffeurid);
            var dl = _mapper.Map<DrivingLicenseEntity>(temp);
            var check = CheckExistingDrivingLicense(chaffeurid, temp);
            var result = new GenericResult<GeneralModels>() { Message = "Drivinglicense already exist's in chaffeurs list." };
            if (check == false)
            {
                return result;
            }

            ch.DrivingLicenses.Add(dl);
            _chrepo.UpdateEntity(ch);
            _chrepo.Save();
            result.Message = "Ok";
            result.SetStatusCode(Overall.ResponseType.OK);
            result.ReturnValue = _mapper.Map<Chauffeur>(ch);
            return result;
        }
        public GenericResult<GeneralModels> DeleteDrivingLicense(int drivinglicense, int chaffeurid)
        {
            var temp = GetChauffeurEntity(chaffeurid);
            var temp2 = temp.DrivingLicenses.FirstOrDefault(s => s.Id == drivinglicense);
            var result = new GenericResult<GeneralModels>() { Message = "Drivinglicense doesn't exist in chaffeurs list." };
            if (temp2 != null)
            {
                temp.DrivingLicenses.Remove(temp2);
                _chrepo.UpdateEntity(temp);
                _chrepo.Save();
                result.SetStatusCode(Overall.ResponseType.OK);
                result.Message = "Ok";
                result.ReturnValue = _mapper.Map<Chauffeur>(temp);
                return result;
            }
            return result;
        }
        public GenericResult<GeneralModels> GetAllDrivingLicenses()
        {
            var temp = _mapper.Map<List<DrivingLicense>>(_repo.GetAll(
                s => s.Include(x => x.Chauffeur)));
            var value = temp == null ? null : _mapper.Map<List<DrivingLicense>>(temp);
            return CreateResult(temp == null, value);
        }
        public GenericResult<GeneralModels> GetAllDrivingLicensesPaging(GenericParameter parameters)
        {
            var temp = _mapper.Map<List<DrivingLicense>>(_repo.GetAllWithPaging(
                s => s.Include(x => x.Chauffeur),parameters));
            var value = temp == null ? null : _mapper.Map<List<DrivingLicense>>(temp);
            return CreateResult(temp == null, value);
        }
        public GenericResult<GeneralModels> GetAllDrivingLicenseById(int id)
        {
            var value = _mapper.Map<DrivingLicense>(_repo.GetById(
                filter: x => x.Id == id,
                including: s => s.Include(x => x.Chauffeur)));

            return CreateResult(value == null, value);
        }
        public GenericResult<GeneralModels> GetDrivingLicenseChaffeurById(int id)
        {
            var temp = _mapper.Map<DrivingLicense>(_repo.GetById(
                filter: x => x.Id == id,
                including: s => s.Include(x => x.Chauffeur)));

            var value = temp == null ? null : temp.Chauffeur;
            return CreateResult(temp == null, value);
        }
        public ChauffeurEntity GetChauffeurEntity(int id)
        {
            var temp = _chrepo.GetById(
            filter: x => x.Id == id
            , x => x
            .Include(s => s.DrivingLicenses));
            return temp;

        }
        public bool CheckExistingDrivingLicense(int id, DrivingLicense license)
        {
            var temp = _mapper.Map<Chauffeur>(GetChauffeurEntity(id));
            return temp.CheckDrivingLicense(license);
        }
        public GenericResult<GeneralModels> CreateResult(bool check, object value)
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
