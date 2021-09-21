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
    public class RepairmentManager : IRepairmentManager
    {
        private readonly IGenericRepo<RequestEntity> _rqrepo;
        private readonly IGenericRepo<RepairmentEntity> _repo;
        private readonly IMapper _mapper;
        public RepairmentManager(IGenericRepo<RequestEntity> rqrepo, IMapper mapper, IGenericRepo<RepairmentEntity> repo)
        {
            this._repo = repo;
            this._rqrepo = rqrepo;
            this._mapper = mapper;
        }
        public void AddRepairment(Repairment repairment, int requestId)
        {
            var rq = GetRequestEntity(requestId);
            var rm = _mapper.Map<RepairmentEntity>(repairment);
            rm.Request = rq;
            rm.RequestId = rq.Id;

            _repo.AddEntity(rm);
            _repo.Save();
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
