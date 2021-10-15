using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models.input
{
    public class MaintenanceDTO
    {
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public string Garage { get; set; }
    }
}
