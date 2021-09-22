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
            ChaffeurVehicles = new List<VehicleChaffeur>();
            Requests = new List<Request>();
            LicensePlates = new List<LicensePlate>();
        }

        public Vehicle(int chassis, CarTypes type, double kilometers, FuelTypes fuel, string brand, string model, DateTime build)
        {
            Chassis = chassis;
            Type = type;
            Kilometers = kilometers;
            ChaffeurVehicles = new List<VehicleChaffeur>();
            Requests = new List<Request>();
            LicensePlates = new List<LicensePlate>();
            FuelType = fuel;
            Brand = brand;
            Model = model;
            BuildDate = build;
        }

        public int Id { get; set; }
        public int Chassis { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime BuildDate { get; set; }
        public CarTypes Type { get; set; }
        public double Kilometers { get; set; }
        public FuelTypes FuelType { get; set; }
        public List<VehicleChaffeur> ChaffeurVehicles { get; set; }
        public List<Request> Requests { get; set; }
        public List<LicensePlate> LicensePlates { get; set; }
    }
}
