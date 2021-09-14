using Overall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class Vehicle
    {
        public Vehicle()
        {
            Chaffeurs = new List<Chaffeur>();
            Requests = new List<Request>();
            FuelTypes = new List<FuelType>();
            LicensePlates = new List<LicensePlate>();
        }

        public Vehicle(int chassis, CarTypes type, double kilometers)
        {
            Chassis = chassis;
            Type = type;
            Kilometers = kilometers;
            Chaffeurs = new List<Chaffeur>();
            Requests = new List<Request>();
            FuelTypes = new List<FuelType>();
            LicensePlates = new List<LicensePlate>();
        }

        public int Id { get; set; }
        public int Chassis { get; set; }
        public CarTypes Type { get; set; }
        public double Kilometers { get; set; }
        public List<Chaffeur> Chaffeurs { get; set; }
        public List<Request> Requests { get; set; }
        public List<FuelType> FuelTypes { get; set; }
        public List<LicensePlate> LicensePlates { get; set; }
    }
}
