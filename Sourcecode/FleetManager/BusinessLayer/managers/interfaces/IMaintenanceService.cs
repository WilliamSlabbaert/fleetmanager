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
    public interface IMaintenanceService
    {
        public List<GenericResponse> _errors { get; set; }
        public GenericResult<IGeneralModels> GetAllMaintenances();
        public GenericResult<IGeneralModels> GetMaintenanceById(int id);
        public GenericResult<IGeneralModels> GetMaintenanceInvoicesById(int id);
        public GenericResult<IGeneralModels> GetMaintenanceRequestById(int id);
        public GenericResult<IGeneralModels> GetAllMaintenancesPaging(GenericParameter parameters);
        public Maintenance AddMaintenance(Maintenance Maintenance, int requestId);
        public Maintenance UpdateMaintenance(Maintenance maintenance, int requestId, int maintenanceId);
        public bool ValidateMaintance(Maintenance maintenance);
    }
}
