using BusinessLayer.models;
using BusinessLayer.validators.response;
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
        public Request AddRequest(Request request, int chaffeurId, int vehicleId);
        public Request UpdateRequest(Request request, int vehicleid, int chaffeurid, int id);
        public Request GetRequestById(int id);
        public List<Request> GetAllRequests();
    }
}
