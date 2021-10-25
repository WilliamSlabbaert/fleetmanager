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
        public GenericResult<GeneralModels> GetFuelCardById(int id);
        public GenericResult<GeneralModels> GetAllFuelCards();
        public GenericResult<GeneralModels> GetFuelcardChauffeurs(int id);
        public GenericResult<GeneralModels> GetFuelcardFuelTypes(int id);
        public GenericResult<GeneralModels> GetFuelcardAuthenications(int id);
        public GenericResult<GeneralModels> GetAllFuelCardsPaging(GenericParameter parameters);
        public GenericResult<GeneralModels> AddFuelType(int fuelcardId, FuelTypeDTO type);
        public GenericResult<GeneralModels> AddFuelCardToChauffeur(int fuelcardNr, int chauffeurNr);
        public GenericResult<GeneralModels> ActivityChauffeurFuelCard(int fuelcardNr, int chauffeurNr, bool isactive);
        public GenericResult<GeneralModels> UpdateFuelCard(int fuelcardNr, FuelCardDTO fuelCard);
        public GenericResult<GeneralModels> AddAuthentication(AuthenticationTypeDTO authenticationType, int fuelcardId);
        public GenericResult<GeneralModels> AddFuelCard(FuelCardDTO fc);
        public GenericResult<GeneralModels> AddService(ExtraServiceDTO extraService, int fuelcardId);
        public GenericResult<GeneralModels> DeleteFuelType(int id, int fuelid);
        public GenericResult<GeneralModels> DeleteService(int fuelcardId, int serviceId);
        public GenericResult<GeneralModels> DeleteAuthentication(int fuelcardId, int authenticationId);
    }
}
