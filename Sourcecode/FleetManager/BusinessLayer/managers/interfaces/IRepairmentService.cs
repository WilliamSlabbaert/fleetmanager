using BusinessLayer.models;
using BusinessLayer.validators.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IRepairmentService
    {
        public List<GenericResponse> _errors { get; set; }
        public void AddRepairment(Repairment repairment, int requestId);
        public List<Repairment> GetAllRepairments();
        public Repairment GetRepairmentById(int id);
    }
}
