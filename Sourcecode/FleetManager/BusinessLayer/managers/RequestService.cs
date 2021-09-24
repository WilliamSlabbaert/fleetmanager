using AutoMapper;
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
    public class RequestService : IRequestService
    {
        private readonly IGenericRepo<RequestEntity> _repo;
        private readonly IGenericRepo<ChaffeurEntity> _chrepo;
        private readonly IGenericRepo<VehicleEntity> _vhrepo;
        private readonly IMapper _mapper;
        private readonly IValidator<Request> _validator;
        public List<GenericResponse> _errors { get; set; }
        public RequestService(IGenericRepo<RequestEntity> repo, IMapper mapper, IGenericRepo<ChaffeurEntity> chrepo, IGenericRepo<VehicleEntity> vhrepo, IValidator<Request> validator)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._chrepo = chrepo;
            this._vhrepo = vhrepo;
            this._validator = validator;
            _errors = new List<GenericResponse>();
        }
        public void AddRequest(Request request, int chaffeurId, int vehicleId)
        {
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurId);
            VehicleEntity vh = GetVehicleEntity(vehicleId);
            var results = _validator.Validate(request);

            if(results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                var rq = _mapper.Map<RequestEntity>(request);
                rq.Chaffeur = ch;
                rq.ChaffeurId = ch.Id;
                rq.Vehicle = vh;
                rq.VehicleId = vh.Id;

                _repo.AddEntity(rq);
                _repo.Save();
            }
        }
        public List<Request> GetAllRequests()
        {
            return _mapper.Map<List<Request>>(_repo.GetAll(
                x => x.Include(s => s.Chaffeur)
                .Include(s => s.Maintenance)
                .Include(s => s.Repairment)
                .Include(s => s.Vehicle)));
        }
        public Request GetRequestById(int id)
        {
            return _mapper.Map<Request>(_repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Chaffeur)
                .Include(s => s.Maintenance)
                .Include(s => s.Repairment)
                .Include(s => s.Vehicle)));
        }

        public VehicleEntity GetVehicleEntity(int id)
        {
            return _vhrepo.GetById(
            filter: f => f.Id == id,
            x => x.Include(s => s.Requests)
            .Include(s => s.ChaffeurVehicles)
            .ThenInclude(s => s.Chaffeur));
        }
        public ChaffeurEntity GetChaffeurEntity(int id)
        {
            return _chrepo.GetById(
            filter: f => f.Id == id,
            x => x.Include(s => s.Requests)
            .Include(s => s.ChaffeurVehicles)
            .ThenInclude(s => s.Vehicle));
        }
    }
}
