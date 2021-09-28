using DataLayer.entities.generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class MaintenanceEntity : IGeneralEntities
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public string Garage { get; set; }
        public List<InvoiceEntity> Invoices { get; set; }
        public int RequestId { get; set; }
        public RequestEntity Request { get; set; }
    }
}
