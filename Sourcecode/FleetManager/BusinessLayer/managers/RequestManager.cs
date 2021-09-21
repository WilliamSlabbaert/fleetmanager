using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using DataLayer.entities;
using DataLayer.repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers
{
    public class RequestManager : IRequestManager
    {
        private readonly IGenericRepo<RequestEntity> _repo;
        private readonly IGenericRepo<ChaffeurEntity> _chrepo;
        private readonly IGenericRepo<VehicleEntity> _vhrepo;
        private readonly IMapper _mapper;
        public RequestManager(IGenericRepo<RequestEntity> repo, IMapper mapper, IGenericRepo<ChaffeurEntity> chrepo, IGenericRepo<VehicleEntity> vhrepo)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._chrepo = chrepo;
            this._vhrepo = vhrepo;
        }
        public void AddRequest(Request request, int chaffeurId, int vehicleId)
        {
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurId);
            VehicleEntity vh = GetVehicleEntity(vehicleId);

            var rq = _mapper.Map<RequestEntity>(request);
            rq.Chaffeur = ch;
            rq.ChaffeurId = ch.Id;
            rq.Vehicle = vh;
            rq.VehicleId = vh.Id;

            _repo.AddEntity(rq);
            _repo.Save();
        }
        public List<Request> GetAllRequests()
        {
            return _mapper.Map<List<Request>>(_repo.GetAll(
                x => x.Include(s=> s.Chaffeur)
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
            try
            {
                return _vhrepo.GetById(
                filter: f => f.Id == id,
                x => x.Include(s => s.Requests)
                .Include(s => s.ChaffeurVehicles)
                .ThenInclude(s => s.Chaffeur));
            }
            catch
            {
                throw new Exception("Vehicle is null.");
            }
        }
        public ChaffeurEntity GetChaffeurEntity(int id)
        {
            try
            {
                return _chrepo.GetById(
                filter: f => f.Id == id,
                x => x.Include(s => s.Requests)
                .Include(s => s.ChaffeurVehicles)
                .ThenInclude(s => s.Vehicle));
            }
            catch
            {
                throw new Exception("Chaffeur is null.");
            }
        }
    }
}
