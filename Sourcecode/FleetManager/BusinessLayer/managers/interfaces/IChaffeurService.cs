using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.models.input;
using BusinessLayer.validators.response;
using FluentValidation.Results;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IChaffeurService 
    {
        public object GetHeaders(GenericParameter parameters);
        public GenericResult<IGeneralModels> GetAllChaffeurs();
        public GenericResult<IGeneralModels> GetChaffeurById(int id);
        public GenericResult<IGeneralModels> GetChaffeurVehicles(int chaffeurId);
        public GenericResult<IGeneralModels> GetChaffeurRequests(int chaffeurId);
        public GenericResult<IGeneralModels> GetChaffeurFuelcards(int chaffeurId);
        public GenericResult<IGeneralModels> GetChaffeurDrivingLicenses(int chaffeurId);
        public GenericResult<IGeneralModels> GetAllChaffeursPaging(GenericParameter parameters);
        public FuelCard GetFuelcardFromChaffeur(Chaffeur chaffeur, int fuelcardId);
        public GenericResult<IGeneralModels> AddChaffeur(ChaffeurDTO ch);
        public GenericResult<IGeneralModels> UpdateVehicleToChaffeur(int chaffeurNr, int vehicleNr, bool active);
        public GenericResult<IGeneralModels> UpdateChaffeur(ChaffeurDTO ch, int id);
        public GenericResult<IGeneralModels> AddVehicleToChaffeur(int chaffeurNr, int vehicleNr);
    }
}
