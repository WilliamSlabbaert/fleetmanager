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

namespace BusinessLayer.services.interfaces
{
    public interface IRepairmentService
    {
        public object GetHeaders(GenericParameter parameters);
        public GenericResult<GeneralModels> GetAllRepairments();
        public GenericResult<GeneralModels> GetRepairmentById(int id);
        public GenericResult<GeneralModels> GetRepairmentRequestById(int id);
        public GenericResult<GeneralModels> GetAllRepairmentsPaging(GenericParameter parameters);
        public GenericResult<GeneralModels> AddRepairment(RepairmentDTO repairment, int requestId);
        public GenericResult<GeneralModels> DeleteRepairment(int requestId, int repairmentId);


    }
}
