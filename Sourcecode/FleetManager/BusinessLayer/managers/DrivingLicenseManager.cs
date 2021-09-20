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
    public class DrivingLicenseManager : IDrivingLicenseManager
    {
        private readonly IGenericRepo<DrivingLicenseEntity> _repo;
        private readonly IGenericRepo<ChaffeurEntity> _chrepo;
        private readonly IMapper _mapper;
        public DrivingLicenseManager(IGenericRepo<DrivingLicenseEntity> repo, IMapper mapper, IGenericRepo<ChaffeurEntity> chrepo)
        {
            this._repo = repo;
            _mapper = mapper;
            _chrepo = chrepo;
        }
        public void AddDrivingLicense(DrivingLicense drivinglicense, int chaffeurid)
        {
            var ch = GetChaffeurEntity(chaffeurid);
            var dl = _mapper.Map<DrivingLicenseEntity>(drivinglicense);
            ch.DrivingLicenses.Add(dl);

            _chrepo.UpdateEntity(ch);
            _chrepo.Save();
        }
        public List<DrivingLicense> GetAllDrivingLicenses()
        {
            return _mapper.Map<List<DrivingLicense>>(_repo.GetAll(
                s => s.Include(x => x.Chaffeur)));
        }
        public DrivingLicense GetAllDrivingLicenseById(int id)
        {
            return _mapper.Map<DrivingLicense>(_repo.GetById(
                filter: x => x.Id == id,
                including: s => s.Include(x => x.Chaffeur)));
        }
        public ChaffeurEntity GetChaffeurEntity(int id)
        {
            try
            {
                return _chrepo.GetById(
                filter: x => x.Id == id
                , x => x.Include(s => s.ChaffeurFuelCards)
                .ThenInclude(s => s.FuelCard)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.Requests)
                .Include(s => s.ChaffeurVehicles)
                .ThenInclude(s => s.Vehicle));
            }
            catch
            {
                throw new Exception("Chaffeur is null");
            }
        }
    }
}
