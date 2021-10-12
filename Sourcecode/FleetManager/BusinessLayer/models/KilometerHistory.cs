using BusinessLayer.models.general;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class KilometerHistory : IGeneralModels
    {
        public int Id { get; set; }
        public double Kilometers { get; set; }
        public DateTime Date { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
