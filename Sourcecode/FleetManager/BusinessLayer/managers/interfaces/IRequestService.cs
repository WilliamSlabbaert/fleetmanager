using BusinessLayer.models;
using BusinessLayer.validators.response;
using DataLayer.entities.paging;
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
        public GenericResult GetRequestById(int id);
        public GenericResult GetRequestChaffeur(int id);
        public GenericResult GetRequestVehicle(int id);
        public GenericResult GetRequestRepairs(int id);
        public GenericResult GetRequestMaintenance(int id);
        public GenericResult GetAllRequests();
        public GenericResult GetAllRequestsPaging(GenericParemeters parameters);
        public Request AddRequest(Request request, int chaffeurId, int vehicleId);
        public Request UpdateRequest(Request request, int vehicleid, int chaffeurid, int id);
    }
}
