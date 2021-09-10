using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class InvoiceEntity
    {
        public int Id { get; set; }
        public string InvoiceImage { get; set; }
        public int MaintenanceId { get; set; }
        public MaintenanceEntity Maintenance { get; set; }
    }
}
