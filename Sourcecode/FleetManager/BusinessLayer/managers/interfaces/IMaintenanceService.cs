using BusinessLayer.models;
using BusinessLayer.validators.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IMaintenanceService
    {
        public List<GenericResponse> _errors { get; set; }
        public void AddMaintenance(Maintenance Maintenance, int requestId);
        public List<Maintenance> GetAllMaintenances();
        public Maintenance GetMaintenanceById(int id);
    }
}
