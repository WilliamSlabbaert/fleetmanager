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
    public class ChaffeurManager : IChaffeurManager
    {
        private readonly IGenericRepo<ChaffeurEntity> _repo;
        private readonly IChaffeurRepo _chRepo;
        private readonly IMapper _mapper;
        public ChaffeurManager(IGenericRepo<ChaffeurEntity> repo, IMapper mapper, IChaffeurRepo chRepo)
        {
            this._repo = repo;
            _mapper = mapper;
            _chRepo = chRepo;
        }

        public void AddChaffeur(Chaffeur ch)
        {
            if(ch != null)
            {
                _repo.AddEntity(_mapper.Map<ChaffeurEntity>(ch));
                _repo.Save();
            }else
            {
                throw new Exception("Chaffeur is null.");
            }

        }

        public Chaffeur GetChaffeurById(int id)
        {
            return _mapper.Map<Chaffeur>(GetAllChaffeurs()
                .Where(s => s.Id == id)
                .FirstOrDefault());
        }

        public void UpdateChaffeur(Chaffeur ch)
        {
            _repo.UpdateEntity(_mapper.Map<ChaffeurEntity>(ch));
            _repo.Save();
        }
        public void AddVehicleToChaffeur(Chaffeur ch, Vehicle vh)
        {
            if (vh != null)
            {
                if (ch.CheckVehicle(vh))
                {
                    _chRepo.AddVehicleToChaffeur(_mapper.Map<ChaffeurEntity>(ch), _mapper.Map<VehicleEntity>(vh));
                }
                else
                {
                    throw new Exception("Vehicle already owned by chaffeur.");
                }
            }
            else
            {
                throw new Exception("Vehicle is null");
            }
        }
        public void RemoveVehicleToChaffeur(Chaffeur ch, Vehicle vh)
        {
            if(vh != null)
            {
                if (ch.CheckVehicle(vh) == false)
                {
                    _chRepo.RemoveVehicleToChaffeur(_mapper.Map<ChaffeurEntity>(ch), _mapper.Map<VehicleEntity>(vh));
                }
                else
                {
                    throw new Exception("Vehicle is not owned by chaffeur.");
                }
            }
            else
            {
                throw new Exception("Vehicle is null");
            }
           
        }

        public List<Chaffeur> GetAllChaffeurs()
        {
            String[] te = new string[] { "vehicles"};
            return _mapper.Map<List<Chaffeur>>(this._repo.GetAll(te));
        }
    }
}
