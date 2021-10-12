﻿using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IFuelCardService
    {
        public List<GenericResponse> _errors { get; set; }
        public object GetHeaders(GenericParameter parameters);
        public GenericResult<IGeneralModels> GetFuelCardById(int id);
        public GenericResult<IGeneralModels> GetAllFuelCards();
        public GenericResult<IGeneralModels> GetFuelcardCHaffeurs(int id);
        public GenericResult<IGeneralModels> GetFuelcardFuelTypes(int id);
        public GenericResult<IGeneralModels> GetFuelcardAuthenications(int id);
        public GenericResult<IGeneralModels> GetAllFuelCardsPaging(GenericParameter parameters);
        public bool CheckExistingFuelCard(FuelCard fc);
        public bool CheckValidationService(ExtraService extraService);
        public bool CheckValidationFuelType(FuelType fueltype);
        public bool CheckValidationFuelCard(FuelCard fuelcard);
        public bool CheckValidationAuthentication(AuthenticationType authenticationType);
        public FuelCard AddAuthentication(AuthenticationType authenticationType, int fuelcardId);
        public FuelCard AddFuelCard(FuelCard fc);
        public FuelCard AddFuelType(int fuelcardId, FuelType type);
        public FuelCard AddService(ExtraService extraService, int fuelcardId);
        public Chaffeur AddFuelCardToChaffeur(int fuelcardNr, int chaffeurNr);
        public FuelCard DeleteFuelType(int id, int fuelid);
        public FuelCard DeleteService(int id, int fuelcardId);
        public Chaffeur UpdateChaffeurFuelCard(int fuelcardNr, int chaffeurNr, bool isactive);
        public FuelCard UpdateFuelCard(FuelCard fuelcard, int fuelcardId);
    }
}
