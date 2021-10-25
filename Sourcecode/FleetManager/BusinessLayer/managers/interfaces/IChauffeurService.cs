using BusinessLayer.models.general;
using BusinessLayer.models.input;
using BusinessLayer.validators.response;
using Overall.paging;

namespace BusinessLayer.managers.interfaces
{
    public interface IChauffeurService 
    {
        public object GetHeaders(GenericParameter parameters);
        public GenericResult<IGeneralModels> GetAllChauffeurs();
        public GenericResult<IGeneralModels> GetChauffeurById(int id);
        public GenericResult<IGeneralModels> GetChauffeurVehicles(int chaffeurId);
        public GenericResult<IGeneralModels> GetChauffeurRequests(int chaffeurId);
        public GenericResult<IGeneralModels> GetChauffeurFuelcards(int chaffeurId);
        public GenericResult<IGeneralModels> GetChauffeurDrivingLicenses(int chaffeurId);
        public GenericResult<IGeneralModels> GetAllChauffeursPaging(GenericParameter parameters);
        public GenericResult<IGeneralModels> AddChauffeur(ChaffeurDTO ch);
        public GenericResult<IGeneralModels> UpdateVehicleToChauffeur(int chaffeurNr, int vehicleNr, bool active);
        public GenericResult<IGeneralModels> UpdateChauffeur(ChaffeurDTO ch, int id);
        public GenericResult<IGeneralModels> AddVehicleToChauffeur(int chaffeurNr, int vehicleNr);
    }
}
