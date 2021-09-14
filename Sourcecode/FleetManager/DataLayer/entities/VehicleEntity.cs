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
        public VehicleEntity()
        {
            Chaffeurs = new List<ChaffeurEntity>();
            Requests = new List<RequestEntity>();
            FuelTypes = new List<FuelEntity>();
        }

        public VehicleEntity(int chassis, CarTypes type, double kilometers)
        {
            Chassis = chassis;
            Type = type;
            Kilometers = kilometers;
            Chaffeurs = new List<ChaffeurEntity>();
            Requests = new List<RequestEntity>();
            FuelTypes = new List<FuelEntity>();
        }

        [Key]
        public int Id { get; set; }
        public int Chassis { get; set; }
        public CarTypes Type { get; set; }
        public double Kilometers { get; set; }
        public List<ChaffeurEntity> Chaffeurs { get; set; }
        public List<RequestEntity> Requests { get; set; }
        public List<FuelEntity> FuelTypes { get; set; }
        public List<LicensePlateEntity> LicensePlates { get; set; }
    }
}
