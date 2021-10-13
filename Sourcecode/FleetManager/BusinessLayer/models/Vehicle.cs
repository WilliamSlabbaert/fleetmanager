using BusinessLayer.models.general;
using Overall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class Vehicle : IGeneralModels
    {
        public Vehicle()
        {
            ChaffeurVehicles = new List<VehicleChaffeur>();
            Requests = new List<Request>();
            LicensePlates = new List<LicensePlate>();
            Kilometers = new List<KilometerHistory>();
        }

        public Vehicle(int chassis, CarTypes type, FuelTypes fuel, string brand, string model, DateTime build)
        {
            Chassis = chassis;
            Type = type;
            Kilometers = new List<KilometerHistory>();
            ChaffeurVehicles = new List<VehicleChaffeur>();
            Requests = new List<Request>();
            LicensePlates = new List<LicensePlate>();
            FuelType = fuel;
            Brand = brand;
            Model = model;
            BuildDate = build;
        }
        public bool CheckLicensePlates(LicensePlate licensePlate)
        {
            var temp = this.LicensePlates.FirstOrDefault(s => s.Plate == licensePlate.Plate && s.Id != licensePlate.Id);
            if (temp == null)
                return true;
            return false;
        }

        public int Id { get; set; }
        public int Chassis { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime BuildDate { get; set; }
        public CarTypes Type { get; set; }
        public FuelTypes FuelType { get; set; }
        public List<KilometerHistory> Kilometers { get; set; }
        public List<VehicleChaffeur> ChaffeurVehicles { get; set; }
        public List<Request> Requests { get; set; }
        public List<LicensePlate> LicensePlates { get; set; }
    }
}
