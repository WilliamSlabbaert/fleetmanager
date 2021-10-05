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
        public GenericResult GetAllChaffeurs();
        public GenericResult GetChaffeurById(int id);
        public FuelCard GetFuelcardFromChaffeur(Chaffeur chaffeur, int fuelcardId);
        public Chaffeur AddChaffeur(Chaffeur ch);
        public Chaffeur UpdateVehicleToChaffeur(int chaffeurNr, int vehicleNr, bool active);
        public bool CheckValidationChaffeur(Chaffeur chaffeur);
        public bool CheckExistingChaffeur(Chaffeur ch, int id);
        public Chaffeur UpdateChaffeur(Chaffeur ch, int id);
        public Chaffeur AddVehicleToChaffeur(int chaffeurNr, int vehicleNr);
    }
}
