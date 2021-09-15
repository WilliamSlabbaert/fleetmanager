using BusinessLayer.models;
using Overall;
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
        public bool IsActive { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<FuelCard> FuelCards { get; set; }
        public List<DrivingLicense> DrivingLicenses { get; set; }
        public List<Request> Requests { get; set; }
        public Chaffeur(string firstName, string lastName, string city, string street, string houseNumber, DateTime dateOfBirth, string nationalInsurenceNumber, bool isActive)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            DateOfBirth = dateOfBirth;
            NationalInsurenceNumber = nationalInsurenceNumber;
            IsActive = isActive;
            Vehicles = new List<Vehicle>();
            FuelCards = new List<FuelCard>();
            DrivingLicenses = new List<DrivingLicense>();
            Requests = new List<Request>();
        }

        public Chaffeur()
        {
            Vehicles = new List<Vehicle>();
            FuelCards = new List<FuelCard>();
            DrivingLicenses = new List<DrivingLicense>();
            Requests = new List<Request>();
        }

        public bool CheckVehicle(Vehicle vehicle)
        {
            if (vehicle != null)
            {
                if (!Vehicles.Contains(vehicle))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
