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
        public Maintenance AddMaintenance(Maintenance Maintenance, int requestId);
        public Maintenance UpdateMaintenance(Maintenance maintenance, int requestId, int maintenanceId);
        public List<Maintenance> GetAllMaintenances();
        public bool ValidateMaintance(Maintenance maintenance);
        public Maintenance GetMaintenanceById(int id);
    }
}
