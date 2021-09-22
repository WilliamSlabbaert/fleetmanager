﻿using Overall;
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
            Requests = new List<RequestEntity>();
            LicensePlates = new List<LicensePlateEntity>();
            ChaffeurVehicles = new List<ChaffeurEntityVehicleEntity>();
        }

        public VehicleEntity(int chassis, CarTypes type, double kilometers, FuelTypes fuel,string brand, string model, DateTime build)
        {
            Chassis = chassis;
            Type = type;
            Kilometers = kilometers;
            Requests = new List<RequestEntity>();
            LicensePlates = new List<LicensePlateEntity>();
            ChaffeurVehicles = new List<ChaffeurEntityVehicleEntity>();
            FuelType = fuel;
            Brand = brand;
            Model = model;
            BuildDate = build;
        }

        [Key]
        public int Id { get; set; }
        public int Chassis { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime BuildDate { get; set; }
        public CarTypes Type { get; set; }
        public double Kilometers { get; set; }
        public FuelTypes FuelType { get; set; }
        public List<ChaffeurEntityVehicleEntity> ChaffeurVehicles{ get; set; }
        public List<RequestEntity> Requests { get; set; }
        public List<LicensePlateEntity> LicensePlates { get; set; }
    }
}
