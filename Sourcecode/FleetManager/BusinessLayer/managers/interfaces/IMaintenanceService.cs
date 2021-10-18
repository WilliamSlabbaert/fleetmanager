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
    public interface IMaintenanceService
    {
        public object GetHeaders(GenericParameter parameters);
        public GenericResult<IGeneralModels> GetAllMaintenances();
        public GenericResult<IGeneralModels> GetMaintenanceById(int id);
        public GenericResult<IGeneralModels> GetMaintenanceInvoicesById(int id);
        public GenericResult<IGeneralModels> GetMaintenanceRequestById(int id);
        public GenericResult<IGeneralModels> GetAllMaintenancesPaging(GenericParameter parameters);
        public GenericResult<IGeneralModels> AddMaintenance(MaintenanceDTO Maintenance, int requestId);
        public GenericResult<IGeneralModels> DeleteMaintenance(int requestid, int maintenanceid);
        public GenericResult<IGeneralModels> UpdateMaintenance(int maintenanceid, MaintenanceDTO maintenance);
        public GenericResult<IGeneralModels> AddInvoice(int maintenanceId, InvoiceDTO invoice);
        public GenericResult<IGeneralModels> DeleteInvoice(int maintenanceId, int invoiceId);
        public bool ValidateMaintance(Maintenance maintenance);
    }
}
