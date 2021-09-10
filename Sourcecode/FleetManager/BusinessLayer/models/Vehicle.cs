using DataLayer.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int Chassis { get; set; }
        public string LicensePlate { get; set; }
        public List<FuelTypes> FuelTypes { get; set; }
        public string Type { get; set; }
        public double Kilometers { get; set; }
        public int ChaffeurId { get; set; }
        public Chaffeur Chaffeur { get; set; }
        public List<Request> Requests { get; set; }

    }
}
