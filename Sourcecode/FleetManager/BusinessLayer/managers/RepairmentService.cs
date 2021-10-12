using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.mediator.commands;
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
    public class RepairmentService : IRepairmentService
    {
        private readonly IGenericRepo<RequestEntity> _rqrepo;
        private readonly IGenericRepo<RepairmentEntity> _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<Repairment> _validator;
        private IMediator _mediator;
        public List<GenericResponse> _errors { get; set; }
        public RepairmentService(IGenericRepo<RequestEntity> rqrepo, IMapper mapper, IGenericRepo<RepairmentEntity> repo, IValidator<Repairment> validator, IMediator mediator)
        {
            this._repo = repo;
            this._rqrepo = rqrepo;
            this._mapper = mapper;
            this._validator = validator;
            this._errors = new List<GenericResponse>();
            this._mediator = mediator;
        }
        public Repairment AddRepairment(Repairment repairment, int requestId)
        {
            var rq = GetRequestEntity(requestId);
            var rm = _mapper.Map<RepairmentEntity>(repairment);
            var results = _validator.Validate(repairment);
            if(results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                rq.Repairment.Add(rm);
                _rqrepo.UpdateEntity(rq);
                _repo.Save();
            }
            return _mapper.Map<Repairment>(rm);
        }
        public Repairment UpdateRepairment(Repairment repairment, int requestId, int repairmentId)
        {
            var rq = GetRequestEntity(requestId);
            var rm = GetRepairmentEntityById(repairmentId);
            var results = _validator.Validate(repairment);
            if (results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                rm.Request = rq;
                rm.RequestId = rq.Id;
                rm.Date = repairment.Date;
                rm.Company = repairment.Company;
                rm.Description = repairment.Description;
                _repo.UpdateEntity(rm);
                _repo.Save();
            }
            return _mapper.Map<Repairment>(rm);
        }


        public GenericResult<IGeneralModels> GetAllRepairments()
        {
            var temp = _mapper.Map<List<Repairment>>(_repo.GetAll(
                x => x.Include(s => s.Request)));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetAllRepairmentsPaging(GenericParameter parameters)
        {
            var temp = _mapper.Map<List<Repairment>>(_repo.GetAllWithPaging(
                x => x.Include(s => s.Request),parameters));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetRepairmentById(int id)
        {
            var temp = _mapper.Map<Repairment>(GetRepairmentEntityById(id));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value);
        }
        public GenericResult<IGeneralModels> GetRepairmentRequestById(int id)
        {
            var temp = _mapper.Map<Repairment>(GetRepairmentEntityById(id));

            var value = temp == null ? null : temp.Request;
            return CreateResult(temp == null, value);
        }

        public RepairmentEntity GetRepairmentEntityById(int id)
        {
            return _repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Request));
        }

        public RequestEntity GetRequestEntity(int id)
        {
            try
            {
                return _rqrepo.GetById(
                filter: x => x.Id == id,
                x => x.Include(x => x.Repairment));
            }
            catch
            {
                throw new Exception("Request is null.");
            }

        }
        public GenericResult<IGeneralModels> CreateResult(bool check, object value)
        {
            var message = "OK";
            var code = Overall.ResponseType.OK;
            if (check)
            {
                message = "Repairment('s) not found";
                code = Overall.ResponseType.NotFound;
                value = null;
            }
            var resp = _mediator.Send(new CreateGenericResultCommand(message, code, value));
            return resp.Result;
        }
    }
}
