using DataLayer.entities.generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class KilometerHistoryEntity : GeneralEntities
    {

        public double Kilometers { get; set; }
        public DateTime Date { get; set; }
        public int VehicleId { get; set; }
        public VehicleEntity Vehicle { get; set; }
    }
}
