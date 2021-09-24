﻿using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
        public List<GenericResponse> _errors { get; set; }
        public RepairmentService(IGenericRepo<RequestEntity> rqrepo, IMapper mapper, IGenericRepo<RepairmentEntity> repo, IValidator<Repairment> validator)
        {
            this._repo = repo;
            this._rqrepo = rqrepo;
            this._mapper = mapper;
            this._validator = validator;
            this._errors = new List<GenericResponse>();
        }
        public void AddRepairment(Repairment repairment, int requestId)
        {
            var rq = GetRequestEntity(requestId);
            var results = _validator.Validate(repairment);
            if(results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                var rm = _mapper.Map<RepairmentEntity>(repairment);
                rq.Repairment.Add(rm);
                _rqrepo.UpdateEntity(rq);
                _repo.Save();
            }

        }

        public List<Repairment> GetAllRepairments()
        {
            return _mapper.Map<List<Repairment>>(_repo.GetAll(
                x => x.Include(s => s.Request)));
        }
        public Repairment GetRepairmentById(int id)
        {
            return _mapper.Map<Repairment>(_repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Request)));
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
    }
}
