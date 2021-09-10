using BusinessLayer.models;
using DataLayer.utility;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class Chaffeur
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalInsurenceNumber { get; set; }
        public List<License> Licenses { get; set; }
        public bool IsActive { get; set; }
        public string FuelCard { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<FuelCardChaffeur> fuelCardChaffeurs { get; set; }
    }
}
