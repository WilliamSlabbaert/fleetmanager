using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using BusinessLayer.validators;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IGenericRepo<VehicleEntity> _vhrepo;
        private readonly IMapper _mapper;
        private readonly IValidator<Chaffeur> _validator;
        public ChaffeurManager(IGenericRepo<ChaffeurEntity> repo, IMapper mapper, IGenericRepo<VehicleEntity> vhrepo, IValidator<Chaffeur> val)
        {
            this._repo = repo;
            _mapper = mapper;
            _vhrepo = vhrepo;
            _validator = val;
        }

        public void AddChaffeur(Chaffeur ch)
        {
            _repo.AddEntity(_mapper.Map<ChaffeurEntity>(ch));
            _repo.Save();
        }

        public Chaffeur GetChaffeurById(int id)
        {
            return _mapper.Map<Chaffeur>(_repo.GetById(
            filter: x => x.Id == id
            , x => x.Include(s => s.ChaffeurFuelCards)
            .ThenInclude(s => s.FuelCard)
            .Include(s => s.DrivingLicenses)
            .Include(s => s.Requests)
            .Include(s => s.ChaffeurVehicles)
            .ThenInclude(s => s.Vehicle)
            ));
        }

        public void UpdateChaffeur(Chaffeur ch)
        {
            _repo.UpdateEntity(_mapper.Map<ChaffeurEntity>(ch));
            _repo.Save();
        }
        public void AddVehicleToChaffeur(int chaffeurNr, int vehicleNr)
        {
            VehicleEntity vh = GetVehicleEntity(vehicleNr);
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurNr);

            var chmodel = _mapper.Map<Chaffeur>(ch);
            if (chmodel.CheckVehicle(vh.Id))
            {
                ch.ChaffeurVehicles.Add(new ChaffeurEntityVehicleEntity(vh, ch, true));
                _repo.Save();
            }
            else
            {
                throw new Exception("Vehicle is already in Chaffeurs list.");
            }
        }

        public void RemoveVehicleToChaffeur(int chaffeurNr, int vehicleNr)
        {

            VehicleEntity vh = GetVehicleEntity(vehicleNr);
            ChaffeurEntity ch = GetChaffeurEntity(chaffeurNr);

            var temp = ch.ChaffeurVehicles.FirstOrDefault(s => s.ChaffeurId == ch.Id && s.VehicleId == vh.Id);

            if (temp != null)
            {
                ch.ChaffeurVehicles.Remove(temp);
                _repo.UpdateEntity(ch);
                _repo.Save();
            }
            else
            {
                throw new Exception("Vehicle is not in Chaffeurs list.");
            }
        }

        public ChaffeurEntity GetChaffeurEntity(int id)
        {
            try
            {
                var ch = _repo.GetById(
                filter: x => x.Id == id
                , x => x.Include(s => s.ChaffeurFuelCards)
                .ThenInclude(s => s.FuelCard)
                .Include(s => s.ChaffeurVehicles)
                .ThenInclude(s => s.Vehicle)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.Requests));
                return ch;
            }
            catch
            {
                throw new Exception("Chaffeur is null.");
            }
        }
        public VehicleEntity GetVehicleEntity(int id)
        {
            try
            {
                var vh = _vhrepo.GetById(
                filter: x => x.Id == id
                , x => x.Include(s => s.LicensePlates)
                .Include(s => s.ChaffeurVehicles)
                .Include(s => s.LicensePlates)
                .Include(s => s.Requests));
                return vh;
            }
            catch
            {
                throw new Exception("Vehicle is null.");
            }
        }
        public List<Chaffeur> GetAllChaffeurs()
        {
            return _mapper.Map<List<Chaffeur>>(this._repo.GetAll(
                x => x.Include(s => s.ChaffeurFuelCards)
                .Include(s => s.ChaffeurVehicles)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.Requests)));
        }

        public List<string> test(Chaffeur ch)
        {
            var results = _validator.Validate(ch);
            List<string> temp = new List<string>();
            if (results.IsValid == false)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    temp.Add(failure.ErrorMessage);
                }
            }
            return results.Errors.Select(s => s.ErrorMessage).ToList();
        }
    }
}
