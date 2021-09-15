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
            _repo.AddEntity(_mapper.Map<ChaffeurEntity>(ch));
            _repo.Save();
        }

        public Chaffeur GetChaffeurById(int id)
        {
            return _mapper.Map<Chaffeur>(this._repo.GetAll()
                .Where(s => s.Id == id)
                .Include(s => s.Vehicles)
                .Include(s => s.Requests)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.FuelCards)
                .SingleOrDefault());
        }

        public void UpdateChaffeur(Chaffeur ch)
        {
            _repo.UpdateEntity(_mapper.Map<ChaffeurEntity>(ch));
            _repo.Save();
        }
        public void AddVehicleToChaffeur(Chaffeur ch, Vehicle vh)
        {
            if (ch.CheckVehicle(vh))
            {
                _chRepo.AddVehicleToChaffeur(_mapper.Map<ChaffeurEntity>(ch), _mapper.Map<VehicleEntity>(vh));
            }
            else
            {
                throw new Exception("fzefez");
            }
        }
        public void RemoveVehicleToChaffeur(Chaffeur ch, Vehicle vh)
        {
            if (ch.CheckVehicle(vh))
            {
                _chRepo.RemoveVehicleToChaffeur(_mapper.Map<ChaffeurEntity>(ch), _mapper.Map<VehicleEntity>(vh));
            }
            else
            {
                throw new Exception("fzefez");
            }
        }

        public List<Chaffeur> GetAllChaffeurs()
        {
            return _mapper.Map<List<Chaffeur>>(this._repo.GetAll()
                .Include(s => s.Requests)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.FuelCards)
                .Include(s => s.Vehicles)
                .ThenInclude(s => s.Chaffeurs));
        }

        public List<Chaffeur> GetAllChaffeursWithoutIncludes()
        {
            return _mapper.Map<List<Chaffeur>>(this._repo.GetAll());
        }

        public void test()
        {
            throw new Exception("fzefez");
        }
    }
}
