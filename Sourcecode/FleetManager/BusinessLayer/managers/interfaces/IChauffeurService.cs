using BusinessLayer.models.general;
using BusinessLayer.models.input;
using BusinessLayer.validators.response;
using Overall.paging;

namespace BusinessLayer.managers.interfaces
{
    public interface IChauffeurService 
    {
        public object GetHeaders(GenericParameter parameters);
        public GenericResult<GeneralModels> GetAllChauffeurs();
        public GenericResult<GeneralModels> GetChauffeurById(int id);
        public GenericResult<GeneralModels> GetChauffeurVehicles(int chaffeurId);
        public GenericResult<GeneralModels> GetChauffeurRequests(int chaffeurId);
        public GenericResult<GeneralModels> GetChauffeurFuelcards(int chaffeurId);
        public GenericResult<GeneralModels> GetChauffeurDrivingLicenses(int chaffeurId);
        public GenericResult<GeneralModels> GetAllChauffeursPaging(GenericParameter parameters);
        public GenericResult<GeneralModels> AddChauffeur(ChauffeurDTO ch);
        public GenericResult<GeneralModels> UpdateVehicleToChauffeur(int chaffeurNr, int vehicleNr, bool active);
        public GenericResult<GeneralModels> UpdateChauffeur(ChauffeurDTO ch, int id);
        public GenericResult<GeneralModels> AddVehicleToChauffeur(int chaffeurNr, int vehicleNr);
    }
}
