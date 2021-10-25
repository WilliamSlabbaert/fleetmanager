﻿using AutoMapper;
using BusinessLayer.managers.interfaces;
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

namespace BusinessLayer.managers
{
    public class RequestService : IRequestService
    {
        private readonly IGenericRepo<RequestEntity> _repo;
        private readonly IGenericRepo<ChauffeurEntity> _chrepo;
        private readonly IGenericRepo<VehicleEntity> _vhrepo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        public List<GenericResponse> _errors { get; set; }
        public RequestService(IGenericRepo<RequestEntity> repo, IMapper mapper, IGenericRepo<ChauffeurEntity> chrepo, IGenericRepo<VehicleEntity> vhrepo, IMediator mediator)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._chrepo = chrepo;
            this._vhrepo = vhrepo;
            this._mediator = mediator;
        }
        public GenericResult<GeneralModels> AddRequest(RequestDTO request, int chaffeurId, int vehicleId)
        {
            ChauffeurEntity ch = GetChauffeurEntity(chaffeurId);
            VehicleEntity vh = GetVehicleEntity(vehicleId);
            var temp = _mapper.Map<Request>(request);
            var rq = _mapper.Map<RequestEntity>(temp);
            rq.Chauffeur = ch;
            rq.ChauffeurId = ch.Id;
            rq.Vehicle = vh;
            rq.VehicleId = vh.Id;
            _repo.AddEntity(rq);
            _repo.Save();

            var resp = _mediator.Send(new CreateGenericResultCommand("Ok", Overall.ResponseType.OK, _mapper.Map<Request>(rq)));
            return resp.Result;
        }
        public GenericResult<GeneralModels> GetAllRequests()
        {
            var temp = _mapper.Map<List<Request>>(_repo.GetAll(
                x => x.Include(s => s.Chauffeur)
                .Include(s => s.Maintenance)
                .Include(s => s.Repairment)
                .Include(s => s.Vehicle)));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value);
        }
        public GenericResult<GeneralModels> GetAllRequestsPaging(GenericParameter parameters)
        {
            var temp = _mapper.Map<List<Request>>(_repo.GetAllWithPaging(
                x => x.Include(s => s.Chauffeur)
                .Include(s => s.Maintenance)
                .Include(s => s.Repairment)
                .Include(s => s.Vehicle), parameters));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value);
        }
        public GenericResult<GeneralModels> GetRequestById(int id)
        {
            var temp = _mapper.Map<Request>(GetRequestEntityById(id));
            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value);
        }
        public GenericResult<GeneralModels> GetRequestChaffeur(int id)
        {
            var temp = _mapper.Map<Request>(GetRequestEntityById(id));
            var value = temp == null ? null : temp.Chauffeur;
            return CreateResult(temp == null, value);
        }
        public GenericResult<GeneralModels> GetRequestVehicle(int id)
        {
            var temp = _mapper.Map<Request>(GetRequestEntityById(id));
            var value = temp == null ? null : temp.Vehicle;
            return CreateResult(temp == null, value);
        }
        public GenericResult<GeneralModels> GetRequestRepairs(int id)
        {
            var temp = _mapper.Map<Request>(GetRequestEntityById(id));
            var value = temp == null ? null : temp.Repairment;
            return CreateResult(temp == null, value);
        }
        public GenericResult<GeneralModels> GetRequestMaintenance(int id)
        {
            var temp = _mapper.Map<Request>(GetRequestEntityById(id));
            var value = temp == null ? null : temp.Maintenance;
            return CreateResult(temp == null, value);
        }
        public VehicleEntity GetVehicleEntity(int id)
        {
            var temp = _vhrepo.GetById(
           filter: f => f.Id == id,
           x => x.Include(s => s.Requests)
           .Include(s => s.ChauffeurVehicles)
           .ThenInclude(s => s.Chauffeur));
            return temp;
        }
        public ChauffeurEntity GetChauffeurEntity(int id)
        {
            var temp = _chrepo.GetById(
            filter: f => f.Id == id,
            x => x.Include(s => s.Requests)
            .Include(s => s.ChauffeurVehicles)
            .ThenInclude(s => s.Vehicle));
            return temp;
        }
        public GenericResult<GeneralModels> UpdateRequest(RequestDTO request, int id)
        {
            var rq = GetRequestEntityById(id);

            rq.StartDate = request.StartDate;
            rq.EndDate = request.EndDate;
            rq.Status = request.Status;
            rq.Type = request.Type;
            _repo.UpdateEntity(rq);
            _repo.Save();

            var resp = _mediator.Send(new CreateGenericResultCommand("Ok", Overall.ResponseType.OK, _mapper.Map<Request>(rq)));
            return resp.Result;
        }
        public RequestEntity GetRequestEntityById(int id)
        {
            var temp = _repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Chauffeur)
                .Include(s => s.Maintenance)
                .Include(s => s.Repairment)
                .Include(s => s.Vehicle));
            return temp;
        }
        public GenericResult<GeneralModels> CreateResult(bool check, object value)
        {
            var message = "OK";
            var code = Overall.ResponseType.OK;
            if (check)
            {
                message = "Request('s) not found";
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
