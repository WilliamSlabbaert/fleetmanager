﻿using AutoMapper;
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
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IGenericRepo<RequestEntity> _rqrepo;
        private readonly IGenericRepo<MaintenanceEntity> _repo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        public MaintenanceService(IGenericRepo<RequestEntity> rqrepo, IMapper mapper, IGenericRepo<MaintenanceEntity> repo,IMediator mediator)
        {
            this._repo = repo;
            this._rqrepo = rqrepo;
            this._mapper = mapper;
            this._mediator = mediator;
        }
        public GenericResult<GeneralModels> AddMaintenance(MaintenanceDTO maintenance, int requestId)
        {
            var rq = GetRequestEntity(requestId);
            var temp = _mapper.Map<Maintenance>(maintenance);
            var rm = _mapper.Map<MaintenanceEntity>(temp);
            rq.Maintenance.Add(rm);
            _rqrepo.UpdateEntity(rq);
            _repo.Save();
            var respond = new GenericResult<GeneralModels>() { ReturnValue = _mapper.Map<Request>(rq), Message = "Ok" };
            respond.SetStatusCode(Overall.ResponseType.OK);
            return respond;
        }
        public GenericResult<GeneralModels> DeleteMaintenance(int requestid, int maintenanceid)
        {
            var request = GetRequestEntity(requestid);
            var maintenance = request.Maintenance.FirstOrDefault(s=> s.Id == maintenanceid);
            request.Maintenance.Remove(maintenance);

            _rqrepo.UpdateEntity(request);
            _rqrepo.Save();

            var respond = new GenericResult<GeneralModels>() { ReturnValue = _mapper.Map<Request>(request), Message = "Ok" };
            respond.SetStatusCode(Overall.ResponseType.OK);
            return respond;
        }
        public GenericResult<GeneralModels> UpdateMaintenance(int maintenanceid, MaintenanceDTO maintenance)
        {
            var temp = _mapper.Map<Maintenance>(maintenance);
            var mt = GetMaintenanceEntityById(maintenanceid);
            mt.Garage = temp.Garage;
            mt.Price = temp.Price;
            mt.Date = temp.Date;

            _repo.UpdateEntity(mt);
            _repo.Save();

            var respond = new GenericResult<GeneralModels>() { ReturnValue = _mapper.Map<Request>(mt.Request), Message = "Ok" };
            respond.SetStatusCode(Overall.ResponseType.OK);
            return respond;
        }

        public GenericResult<GeneralModels> GetAllMaintenances()
        {
            var temp = _mapper.Map<List<Maintenance>>(_repo.GetAll(
                x => x.Include(s => s.Request)));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetAllMaintenancesPaging(GenericParameter parameters)
        {
            var temp = _mapper.Map<List<Maintenance>>(_repo.GetAllWithPaging(
                x => x.Include(s => s.Request),parameters));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetMaintenanceById(int id)
        {
            var temp = _mapper.Map<Maintenance>(_repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Request)));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetMaintenanceInvoicesById(int id)
        {
            var temp = _mapper.Map<Maintenance>(_repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Request)));

            var value = temp == null ? null : temp.Invoices;
            return CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetMaintenanceRequestById(int id)
        {
            var temp = _mapper.Map<Maintenance>(_repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Request)));

            var value = temp == null ? null : temp.Request;
            return CreateResult(temp == null, value).Result;
        }
        public MaintenanceEntity GetMaintenanceEntityById(int id)
        {
            return _repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Request)
                .Include(s => s.Invoices));
        }

        public RequestEntity GetRequestEntity(int id)
        {
            return _rqrepo.GetById(
            filter: x => x.Id == id,
            x => x.Include(x => x.Maintenance));
        }
        public GenericResult<GeneralModels> AddInvoice(int maintenanceId, InvoiceDTO invoice)
        {
            var temp = _mapper.Map<Invoice>(invoice);
            var maintenance = GetMaintenanceEntityById(maintenanceId);
            maintenance.Invoices.Add(_mapper.Map<InvoiceEntity>(temp));
            _repo.UpdateEntity(maintenance);
            _repo.Save();

            var respond = new GenericResult<GeneralModels>() { ReturnValue = _mapper.Map<Request>(maintenance.Request), Message = "Ok" };
            respond.SetStatusCode(Overall.ResponseType.OK);
            return respond;
        }
        public GenericResult<GeneralModels> DeleteInvoice(int maintenanceId, int invoiceId)
        {
            var maintenance = GetMaintenanceEntityById(maintenanceId);
            var invoice = maintenance.Invoices.FirstOrDefault(s => s.Id == invoiceId);
            maintenance.Invoices.Remove(invoice);
            var respond = new GenericResult<GeneralModels>() { Message = "Invoice doesn't exist in maintenance list." };
            if(invoice == null)
            {
                return respond;
            }
            _repo.UpdateEntity(maintenance);
            _repo.Save();

            respond.Message = "Ok";
            respond.ReturnValue = _mapper.Map<Request>(maintenance.Request);
            respond.SetStatusCode(Overall.ResponseType.OK);
            return respond;
        }
        public async Task<GenericResult<GeneralModels>>CreateResult(bool check, object value)
        {
            var message = "OK";
            var code = Overall.ResponseType.OK;
            if (check)
            {
                message = "Maintenance('s) not found";
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
