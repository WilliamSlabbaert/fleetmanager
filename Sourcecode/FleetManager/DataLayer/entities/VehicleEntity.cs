using DataLayer.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class VehicleEntity
    {
        public int Id { get; set; }
        public int Chassis { get; set; }
        public string LicensePlate { get; set; }
        public List<FuelTypes> FuelTypes { get; set; }
        public string Type { get; set; }
        public double Kilometers { get; set; }
        public int ChaffeurId { get; set; }
        public ChaffeurEntity Chaffeur { get; set; }
        public List<RequestEntity> Requests { get; set; }

    }
}
