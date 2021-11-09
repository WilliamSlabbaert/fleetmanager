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
using BusinessLayer.validators;

namespace BusinessLayer.services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IGenericRepo<RequestEntity> _rqrepo;
        private readonly IGenericRepo<MaintenanceEntity> _repo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        private IValidator<Maintenance> _maintenanceValidator;
        private IValidator<Invoice> _invoiceValidator;
        public MaintenanceService(IGenericRepo<RequestEntity> rqrepo, IMapper mapper, IGenericRepo<MaintenanceEntity> repo,IMediator mediator,
            MaintenanceValidator maintenanceV,
            InvoiceValidator invoiceV)
        {
            this._repo = repo;
            this._rqrepo = rqrepo;
            this._mapper = mapper;
            this._mediator = mediator;
            this._maintenanceValidator = maintenanceV;
            this._invoiceValidator = invoiceV;
        }
        public GenericResult<GeneralModels> AddMaintenance(MaintenanceDTO dto, int requestId)
        {
            var requestEntity = GetRequestEntity(requestId);
            var maintenance = _mapper.Map<Maintenance>(dto);
            var check = _maintenanceValidator.Validate(maintenance);
            var result = GenericValidationCheck.CheckModel(check, "Maintenance is invalid");
            if (check.IsValid)
            {
                var rm = _mapper.Map<MaintenanceEntity>(maintenance);
                requestEntity.Maintenance.Add(rm);
                _rqrepo.UpdateEntity(requestEntity);
                _repo.Save();
                var respond = new GenericResult<GeneralModels>() { ReturnValue = _mapper.Map<Maintenance>(requestEntity.Maintenance.Last()), Message = "Ok" };
                respond.SetStatusCode(Overall.ResponseType.OK);
                return respond;
            }
            return result;
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
        public GenericResult<GeneralModels> UpdateMaintenance(int maintenanceid, MaintenanceDTO dto)
        {
            var maintenance = _mapper.Map<Maintenance>(dto);
            var check = _maintenanceValidator.Validate(maintenance);
            var result = GenericValidationCheck.CheckModel(check, "Maintenance is invalid");
            if (check.IsValid)
            {
                var mt = GetMaintenanceEntityById(maintenanceid);
                mt.Garage = maintenance.Garage;
                mt.Price = maintenance.Price;
                mt.Date = maintenance.Date;

                _repo.UpdateEntity(mt);
                _repo.Save();

                var respond = new GenericResult<GeneralModels>() { ReturnValue = _mapper.Map<Request>(mt.Request), Message = "Ok" };
                respond.SetStatusCode(Overall.ResponseType.OK);
                return respond;
            }
            return result;
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
        public GenericResult<GeneralModels> AddInvoice(int maintenanceId, InvoiceDTO dto)
        {
            var invoice = _mapper.Map<Invoice>(dto);
            var check = _invoiceValidator.Validate(invoice);
            var result = GenericValidationCheck.CheckModel(check, "Invoice is invalid");
            if (check.IsValid)
            {
                var maintenance = GetMaintenanceEntityById(maintenanceId);
                maintenance.Invoices.Add(_mapper.Map<InvoiceEntity>(invoice));
                _repo.UpdateEntity(maintenance);
                _repo.Save();

                var respond = new GenericResult<GeneralModels>() { ReturnValue = _mapper.Map<Request>(maintenance.Request), Message = "Ok" };
                respond.SetStatusCode(Overall.ResponseType.OK);
                return respond;
            }
            return result;
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
