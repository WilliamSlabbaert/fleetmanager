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
        public GenericResult GetAllMaintenances();
        public GenericResult GetMaintenanceById(int id);
        public GenericResult GetMaintenanceInvoicesById(int id);
        public GenericResult GetMaintenanceRequestById(int id);
        public Maintenance AddMaintenance(Maintenance Maintenance, int requestId);
        public Maintenance UpdateMaintenance(Maintenance maintenance, int requestId, int maintenanceId);
        public bool ValidateMaintance(Maintenance maintenance);
    }
}
