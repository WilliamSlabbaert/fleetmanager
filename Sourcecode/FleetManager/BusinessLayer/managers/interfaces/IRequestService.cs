using BusinessLayer.models;
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
    public interface IRequestService
    {
        public List<GenericResponse> _errors { get; set; }
        public GenericResult<IGeneralModels> GetRequestById(int id);
        public GenericResult<IGeneralModels> GetRequestChaffeur(int id);
        public GenericResult<IGeneralModels> GetRequestVehicle(int id);
        public GenericResult<IGeneralModels> GetRequestRepairs(int id);
        public GenericResult<IGeneralModels> GetRequestMaintenance(int id);
        public GenericResult<IGeneralModels> GetAllRequests();
        public GenericResult<IGeneralModels> GetAllRequestsPaging(GenericParameter parameters);
        public Request AddRequest(Request request, int chaffeurId, int vehicleId);
        public Request UpdateRequest(Request request, int vehicleid, int chaffeurid, int id);
    }
}
