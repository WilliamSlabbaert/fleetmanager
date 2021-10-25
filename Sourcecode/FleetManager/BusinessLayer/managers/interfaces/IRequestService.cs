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
    public interface IRequestService
    {
        public object GetHeaders(GenericParameter parameters);
        public GenericResult<GeneralModels> GetRequestById(int id);
        public GenericResult<GeneralModels> GetRequestChaffeur(int id);
        public GenericResult<GeneralModels> GetRequestVehicle(int id);
        public GenericResult<GeneralModels> GetRequestRepairs(int id);
        public GenericResult<GeneralModels> GetRequestMaintenance(int id);
        public GenericResult<GeneralModels> GetAllRequests();
        public GenericResult<GeneralModels> GetAllRequestsPaging(GenericParameter parameters);
        public GenericResult<GeneralModels> AddRequest(RequestDTO request, int chaffeurId, int vehicleId);
        public GenericResult<GeneralModels> UpdateRequest(RequestDTO request, int id);
    }
}
