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
        public GenericResult<GeneralModels> GetAllMaintenances();
        public GenericResult<GeneralModels> GetMaintenanceById(int id);
        public GenericResult<GeneralModels> GetMaintenanceInvoicesById(int id);
        public GenericResult<GeneralModels> GetMaintenanceRequestById(int id);
        public GenericResult<GeneralModels> GetAllMaintenancesPaging(GenericParameter parameters);
        public GenericResult<GeneralModels> AddMaintenance(MaintenanceDTO Maintenance, int requestId);
        public GenericResult<GeneralModels> DeleteMaintenance(int requestid, int maintenanceid);
        public GenericResult<GeneralModels> UpdateMaintenance(int maintenanceid, MaintenanceDTO maintenance);
        public GenericResult<GeneralModels> AddInvoice(int maintenanceId, InvoiceDTO invoice);
        public GenericResult<GeneralModels> DeleteInvoice(int maintenanceId, int invoiceId);
    }
}
