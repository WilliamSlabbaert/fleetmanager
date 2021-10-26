using BusinessLayer.models;
using BusinessLayer.models.general;
using Overall;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class Chauffeur : GeneralModels
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalInsurenceNumber { get; set; }
        public bool IsActive { get; set; }
        public List<VehicleChauffeur> ChaffeurVehicles { get; set; }
        public List<FuelCardChauffeur> ChaffeurFuelCards { get; set; }
        public List<DrivingLicense> DrivingLicenses { get; set; }
        public List<Request> Requests { get; set; }
        public Chauffeur(string firstName, string lastName, string city, string street, string houseNumber, DateTime dateOfBirth, string nationalInsurenceNumber, bool isActive)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            DateOfBirth = dateOfBirth;
            NationalInsurenceNumber = nationalInsurenceNumber;
            IsActive = isActive;

            ChaffeurFuelCards = new List<FuelCardChauffeur>();
            ChaffeurVehicles = new List<VehicleChauffeur>();
            DrivingLicenses = new List<DrivingLicense>();
            Requests = new List<Request>();
        }
        public Chauffeur(string firstName, string lastName, string city, string street, string houseNumber, DateTime dateOfBirth, string nationalInsurenceNumber, bool isActive, DrivingLicense dl)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            DateOfBirth = dateOfBirth;
            NationalInsurenceNumber = nationalInsurenceNumber;
            IsActive = isActive;

            ChaffeurFuelCards = new List<FuelCardChauffeur>();
            ChaffeurVehicles = new List<VehicleChauffeur>();
            DrivingLicenses = new List<DrivingLicense>();
            if (CheckDrivingLicense(dl))
            {
                DrivingLicenses.Add(dl);
            }
            Requests = new List<Request>();
        }

        public Chauffeur()
        {
            ChaffeurFuelCards = new List<FuelCardChauffeur>();
            DrivingLicenses = new List<DrivingLicense>();
            ChaffeurVehicles = new List<VehicleChauffeur>();
            Requests = new List<Request>();
        }
        public bool CheckVehicle(int vehicle)
        {
            if (ChaffeurVehicles.FirstOrDefault(s=>s.Vehicle.Id == vehicle) == null)
            {
                return true;
            }
            return false;
        }
        public bool CheckFuelCard(int fuelcard)
        {
            if (ChaffeurFuelCards.FirstOrDefault(s => s.FuelCard.Id == fuelcard) == null)
            {
                return true;
            }
            return false;
        }
        public bool CheckDrivingLicense(DrivingLicense drivinglicense)
        {
            if (DrivingLicenses.FirstOrDefault(s => s.type == drivinglicense.type) == null)
            {
                return true;
            }
            return false;
        }
    }
}
