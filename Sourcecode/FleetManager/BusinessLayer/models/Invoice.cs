using BusinessLayer.models.general;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class Invoice : GeneralModels
    {
        public string InvoiceImage { get; set; }
        public int MaintenanceId { get; set; }
        public Maintenance Maintenance { get; set; }
    }
}
