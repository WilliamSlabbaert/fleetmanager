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
    public class RepairmentService : IRepairmentService
    {
        private readonly IGenericRepo<RequestEntity> _rqrepo;
        private readonly IGenericRepo<RepairmentEntity> _repo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        public RepairmentService(IGenericRepo<RequestEntity> rqrepo, IMapper mapper, IGenericRepo<RepairmentEntity> repo, IMediator mediator)
        {
            this._repo = repo;
            this._rqrepo = rqrepo;
            this._mapper = mapper;
            this._mediator = mediator;
        }
        public GenericResult<GeneralModels> AddRepairment(RepairmentDTO repairment, int requestId)
        {
            var temp = _mapper.Map<Repairment>(repairment);
            var rq = GetRequestEntity(requestId);
            var rm = _mapper.Map<RepairmentEntity>(temp);
            rq.Repairment.Add(rm);
            _rqrepo.UpdateEntity(rq);
            _rqrepo.Save();
            var respond = new GenericResult<GeneralModels>() { ReturnValue = _mapper.Map<Request>(rq), Message = "Ok" };
            respond.SetStatusCode(Overall.ResponseType.OK);

            return respond;
        }
        public GenericResult<GeneralModels> DeleteRepairment(int requestId, int repairmentId)
        {
            var rq = GetRequestEntity(requestId);
            var rm = GetRepairmentEntityById(repairmentId);
            rq.Repairment.Remove(rm);
            _rqrepo.UpdateEntity(rq); 
            _rqrepo.Save();
            var respond = new GenericResult<GeneralModels>() { ReturnValue = _mapper.Map<Request>(rq), Message = "Ok" };
            respond.SetStatusCode(Overall.ResponseType.OK);

            return respond;
        }


        public GenericResult<GeneralModels> GetAllRepairments()
        {
            var temp = _mapper.Map<List<Repairment>>(_repo.GetAll(
                x => x.Include(s => s.Request)));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetAllRepairmentsPaging(GenericParameter parameters)
        {
            var temp = _mapper.Map<List<Repairment>>(_repo.GetAllWithPaging(
                x => x.Include(s => s.Request), parameters));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetRepairmentById(int id)
        {
            var temp = _mapper.Map<Repairment>(GetRepairmentEntityById(id));

            var value = temp == null ? null : temp;
            return CreateResult(temp == null, value).Result;
        }
        public GenericResult<GeneralModels> GetRepairmentRequestById(int id)
        {
            var temp = _mapper.Map<Repairment>(GetRepairmentEntityById(id));

            var value = temp == null ? null : temp.Request;
            return CreateResult(temp == null, value).Result;
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
        public async Task<GenericResult<GeneralModels>> CreateResult(bool check, object value)
        {
            var message = "OK";
            var code = Overall.ResponseType.OK;
            if (check)
            {
                message = "Repairment('s) not found";
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
