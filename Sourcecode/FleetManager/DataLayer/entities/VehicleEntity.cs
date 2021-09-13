using Overall;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class VehicleEntity
    {
        [Key]
        public int Id { get; set; }
        public int Chassis { get; set; }
        public string LicensePlate { get; set; }
        public string Type { get; set; }
        public double Kilometers { get; set; }
        public int ChaffeurId { get; set; }
        public List<ChaffeurEntity> Chaffeurs { get; set; }
        public List<RequestEntity> Requests { get; set; }
        public List<FuelEntity> FuelTypes { get; set; }
        public List<CarTypeEntity> CarType { get; set; }

    }
}
