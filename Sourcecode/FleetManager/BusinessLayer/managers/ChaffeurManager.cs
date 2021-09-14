using AutoMapper;
using BusinessLayer.managers.interfaces;
using DataLayer.entities;
using DataLayer.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers
{
    public class ChaffeurManager : IChaffeurManager
    {
        private readonly IGenericRepo<ChaffeurEntity> _chaffeurRepo;
        private readonly IMapper _mapper;
        public ChaffeurManager(IGenericRepo<ChaffeurEntity> chaffeurRepo, IMapper mapper)
        {
            this._chaffeurRepo = chaffeurRepo;
            _mapper = mapper;
        }

        public Chaffeur GetChaffeurById(int id)
        {
            return _mapper.Map<Chaffeur>(this._chaffeurRepo.GetById(id)); 
        }

        public string test()
        {
            return "testing";
        }
    }
}
