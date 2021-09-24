using AutoMapper;
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
    public class MaintenanceService
    {
        private readonly IGenericRepo<RequestEntity> _rqrepo;
        private readonly IGenericRepo<MaintenanceEntity> _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<Maintenance> _validator;
        public List<GenericResponse> _errors { get; set; }
        public MaintenanceService(IGenericRepo<RequestEntity> rqrepo, IMapper mapper, IGenericRepo<MaintenanceEntity> repo, IValidator<Maintenance> validator)
        {
            this._repo = repo;
            this._rqrepo = rqrepo;
            this._mapper = mapper;
            this._validator = validator;
            this._errors = new List<GenericResponse>();
        }
        public void AddMaintenance(Maintenance Maintenance, int requestId)
        {
            var rq = GetRequestEntity(requestId);
            var results = _validator.Validate(Maintenance);
            if (results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                var rm = _mapper.Map<MaintenanceEntity>(Maintenance);
                rq.Maintenance.Add(rm);
                _rqrepo.UpdateEntity(rq);
                _repo.Save();
            }

        }

        public List<Maintenance> GetAllMaintenances()
        {
            return _mapper.Map<List<Maintenance>>(_repo.GetAll(
                x => x.Include(s => s.Request)));
        }
        public Maintenance GetMaintenanceById(int id)
        {
            return _mapper.Map<Maintenance>(_repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Request)));
        }

        public RequestEntity GetRequestEntity(int id)
        {
            try
            {
                return _rqrepo.GetById(
                filter: x => x.Id == id,
                x => x.Include(x => x.Maintenance));
            }
            catch
            {
                throw new Exception("Request is null.");
            }

        }
    }
}
