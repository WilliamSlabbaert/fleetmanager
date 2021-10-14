﻿using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.models.general;
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

namespace BusinessLayer.managers
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IGenericRepo<RequestEntity> _rqrepo;
        private readonly IGenericRepo<MaintenanceEntity> _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<Maintenance> _validator;
        private IMediator _mediator;
        public List<GenericResponse> _errors { get; set; }
        public MaintenanceService(IGenericRepo<RequestEntity> rqrepo, IMapper mapper, IGenericRepo<MaintenanceEntity> repo, IValidator<Maintenance> validator,IMediator mediator)
        {
            this._repo = repo;
            this._rqrepo = rqrepo;
            this._mapper = mapper;
            this._validator = validator;
            this._mediator = mediator;
            this._errors = new List<GenericResponse>();
        }
        public GenericResult<IGeneralModels> AddMaintenance(Maintenance maintenance, int requestId)
        {
            var rq = GetRequestEntity(requestId);
            var rm = _mapper.Map<MaintenanceEntity>(maintenance);
            rq.Maintenance.Add(rm);
            _rqrepo.UpdateEntity(rq);
            _repo.Save();
            var respond = new GenericResult<IGeneralModels>() { ReturnValue = _mapper.Map<Request>(rq), Message = "Ok" };
            respond.SetStatusCode(Overall.ResponseType.OK);
            return respond;
        }
        public bool ValidateMaintance(Maintenance maintenance)
        {
            var results = _validator.Validate(maintenance);
            if (results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
                return false;
            }
            return true;
        }
        public GenericResult<IGeneralModels> DeleteMaintenance(int requestid, int maintenanceid)
        {
            var request = GetRequestEntity(requestid);
            var maintenance = request.Maintenance.FirstOrDefault(s=> s.Id == maintenanceid);
            request.Maintenance.Remove(maintenance);

            _rqrepo.UpdateEntity(request);
            _rqrepo.Save();

            var respond = new GenericResult<IGeneralModels>() { ReturnValue = _mapper.Map<Request>(request), Message = "Ok" };
            respond.SetStatusCode(Overall.ResponseType.OK);
            return respond;
        }

        public GenericResult<IGeneralModels> GetAllMaintenances()
        {
            var temp = _mapper.Map<List<Maintenance>>(_repo.GetAll(
                x => x.Include(s => s.Request)));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetAllMaintenancesPaging(GenericParameter parameters)
        {
            var temp = _mapper.Map<List<Maintenance>>(_repo.GetAllWithPaging(
                x => x.Include(s => s.Request),parameters));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetMaintenanceById(int id)
        {
            var temp = _mapper.Map<Maintenance>(_repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Request)));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetMaintenanceInvoicesById(int id)
        {
            var temp = _mapper.Map<Maintenance>(_repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Request)));

            var value = temp == null ? null : temp.Invoices;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetMaintenanceRequestById(int id)
        {
            var temp = _mapper.Map<Maintenance>(_repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Request)));

            var value = temp == null ? null : temp.Request;
            return CreateResult(temp == null, value);
        }
        public MaintenanceEntity GetMaintenanceEntityById(int id)
        {
            return _repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Request));
        }

        public RequestEntity GetRequestEntity(int id)
        {
            return _rqrepo.GetById(
            filter: x => x.Id == id,
            x => x.Include(x => x.Maintenance));
        }
        public GenericResult<IGeneralModels> CreateResult(bool check, object value)
        {
            var message = "OK";
            var code = Overall.ResponseType.OK;
            if (check)
            {
                message = "Maintenance('s) not found";
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
