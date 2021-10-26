using BusinessLayer.models.general;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class Maintenance : GeneralModels
    {
        public Maintenance(DateTime date, double price, string garage)
        {
            Date = date;
            Price = price;
            Garage = garage;
        }
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public double Price { get; set; }
        public string Garage { get; set; }
        public List<Invoice> Invoices { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }
    }
}
