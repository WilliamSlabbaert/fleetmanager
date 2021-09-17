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
    public class FuelCardManager : IFuelCardManager
    {
        private readonly IGenericRepo<FuelCardEntity> _repo;
        private readonly IChaffeurRepo _chRepo;
        private readonly IMapper _mapper;
        public FuelCardManager(IGenericRepo<FuelCardEntity> repo, IMapper mapper, IChaffeurRepo chRepo)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._chRepo = chRepo;
        }
        public void AddFuelCard(FuelCard fc, Chaffeur ch)
        {
            _repo.AddEntity(_mapper.Map<FuelCardEntity>(fc));
        }

        public List<FuelCard> GetAllFuelCards()
        {
            return _mapper.Map<List<FuelCard>>(this._repo.GetAll(x => x.Chaffeurs, x => x.Services, x => x.FuelType, x => x.AuthenthicationCode));
        }

        public FuelCard GetFuelCardById(int id)
        {
            return _mapper.Map<FuelCard>(GetAllFuelCards()
                .Where(s => s.Id == id)
                .FirstOrDefault());
        }

        public void UpdateFuelCard(FuelCard fc)
        {
            this._repo.UpdateEntity(_mapper.Map<FuelCardEntity>(fc));
        }
    }
}
