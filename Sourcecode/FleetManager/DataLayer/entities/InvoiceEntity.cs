using DataLayer.entities.generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class InvoiceEntity : GeneralEntities
    {

        public string InvoiceImage { get; set; }
        public int MaintenanceId { get; set; }
        public MaintenanceEntity Maintenance { get; set; }
    }
}
