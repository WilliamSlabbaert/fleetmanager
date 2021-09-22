using BusinessLayer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IRequestService
    {
        public void AddRequest(Request request, int chaffeurId, int vehicleId);
        public Request GetRequestById(int id);
        public List<Request> GetAllRequests();
    }
}
