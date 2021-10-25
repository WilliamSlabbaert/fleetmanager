using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.models.input;
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
        public object GetHeaders(GenericParameter parameters);
        public GenericResult<IGeneralModels> GetFuelCardById(int id);
        public GenericResult<IGeneralModels> GetAllFuelCards();
        public GenericResult<IGeneralModels> GetFuelcardChauffeurs(int id);
        public GenericResult<IGeneralModels> GetFuelcardFuelTypes(int id);
        public GenericResult<IGeneralModels> GetFuelcardAuthenications(int id);
        public GenericResult<IGeneralModels> GetAllFuelCardsPaging(GenericParameter parameters);
        public GenericResult<IGeneralModels> AddFuelType(int fuelcardId, FuelTypeDTO type);
        public GenericResult<IGeneralModels> AddFuelCardToChauffeur(int fuelcardNr, int chauffeurNr);
        public GenericResult<IGeneralModels> ActivityChauffeurFuelCard(int fuelcardNr, int chauffeurNr, bool isactive);
        public GenericResult<IGeneralModels> UpdateFuelCard(int fuelcardNr, FuelCardDTO fuelCard);
        public GenericResult<IGeneralModels> AddAuthentication(AuthenticationTypeDTO authenticationType, int fuelcardId);
        public GenericResult<IGeneralModels> AddFuelCard(FuelCardDTO fc);
        public GenericResult<IGeneralModels> AddService(ExtraServiceDTO extraService, int fuelcardId);
        public GenericResult<IGeneralModels> DeleteFuelType(int id, int fuelid);
        public GenericResult<IGeneralModels> DeleteService(int fuelcardId, int serviceId);
        public GenericResult<IGeneralModels> DeleteAuthentication(int fuelcardId, int authenticationId);
    }
}
