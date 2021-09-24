using BusinessLayer.models;
using BusinessLayer.validators.response;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IChaffeurService 
    {
        public List<GenericResponse> _errors { get; set; }
        public Chaffeur GetChaffeurById(int id);
        public void AddChaffeur(Chaffeur ch);
        public void UpdateChaffeur(Chaffeur ch);
        public void AddVehicleToChaffeur(int chnr, int vhnr);
        public List<Chaffeur> GetAllChaffeurs();
    }
}
