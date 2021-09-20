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
        private readonly IGenericRepo<ChaffeurEntity> _chrepo;
        private readonly IMapper _mapper;
        public FuelCardManager(IGenericRepo<FuelCardEntity> repo, IMapper mapper, IGenericRepo<ChaffeurEntity> chrepo)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._chrepo = chrepo;

        }

        public void AddFuelCard(FuelCard fc)
        {
            _repo.AddEntity(_mapper.Map<FuelCardEntity>(fc));
            _repo.Save();
        }

        public void AddFuelCardToChaffeur(int fuelcardNr, int chaffeurNr)
        {
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurNr);
            FuelCardEntity fc = GetFuelCardEntity(fuelcardNr);
            var tempch = _mapper.Map<Chaffeur>(ch);

            if (tempch.CheckFuelCard(fc.Id))
            {
                ch.ChaffeurFuelCards.Add(new ChaffeurEntityFuelCardEntity(ch, fc, true));
                _repo.Save();
            }
            else
            {
                throw new Exception("Fuelcard already in chaffeur list.");
            }
        }
        public void RemoveFuelCardFromChaffeur(int fuelcardNr, int chaffeurNr)
        {
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurNr);
            FuelCardEntity fc = GetFuelCardEntity(fuelcardNr);
            var temp = ch.ChaffeurFuelCards.FirstOrDefault(s => s.FuelCardId == fc.Id);
            if (temp != null)
            {
                ch.ChaffeurFuelCards.Remove(temp);
                _chrepo.UpdateEntity(ch);
                _repo.Save();
            }
            else
            {
                throw new Exception("Fuelcard is not in chaffeur list.");
            }
        }

        public List<FuelCard> GetAllFuelCards()
        {
            return _mapper.Map<List<FuelCard>>(this._repo.GetAll(
                x => x.Include(s => s.Services)
                .Include(s => s.FuelType)
                .Include(s => s.AuthenticationTypes)
                .Include(s => s.ChaffeurFuelCards)));
        }

        public FuelCard GetFuelCardById(int id)
        {
            return _mapper.Map<FuelCard>(_repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Services)
                .Include(s => s.FuelType)
                .Include(s => s.AuthenticationTypes)
                .Include(s => s.ChaffeurFuelCards)
                .ThenInclude(s => s.Chaffeur)));
        }
        public FuelCardEntity GetFuelCardEntity(int id)
        {
            try
            {
                return _repo.GetById(
                filter: x => x.Id == id,
                x => x.Include(s => s.Services)
                .Include(s => s.FuelType)
                .Include(s => s.ChaffeurFuelCards)
                .ThenInclude(s => s.Chaffeur));
            }
            catch
            {
                throw new Exception("Fuelcard is null.");
            }
        }
        public ChaffeurEntity GetChaffeurEntity(int id)
        {
            try
            {
                var ch = _chrepo.GetById(
                filter: x => x.Id == id
                , x => x.Include(s => s.ChaffeurFuelCards)
                .Include(s => s.ChaffeurVehicles)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.Requests));
                return ch;
            }
            catch
            {
                throw new Exception("Chaffeur is null.");
            }
        }
        public void UpdateFuelCard(FuelCard fc)
        {
            this._repo.UpdateEntity(_mapper.Map<FuelCardEntity>(fc));
        }
    }
}
