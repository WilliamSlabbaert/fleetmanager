﻿using DataLayer.entities.generic;
using Overall;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class ChaffeurEntity : IGeneralEntities
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalInsurenceNumber { get; set; }
        public bool IsActive { get; set; }
        public List<ChaffeurEntityVehicleEntity> ChaffeurVehicles { get; set; }
        public List<ChaffeurEntityFuelCardEntity> ChaffeurFuelCards { get; set; }
        public List<DrivingLicenseEntity> DrivingLicenses { get; set; }
        public List<RequestEntity> Requests { get; set; }
        public ChaffeurEntity(string firstName, string lastName, string city, string street, string houseNumber, DateTime dateOfBirth, string nationalInsurenceNumber, bool isActive)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            DateOfBirth = dateOfBirth;
            NationalInsurenceNumber = nationalInsurenceNumber;
            IsActive = isActive;
            ChaffeurVehicles = new List<ChaffeurEntityVehicleEntity>();
            ChaffeurFuelCards = new List<ChaffeurEntityFuelCardEntity>();
            DrivingLicenses = new List<DrivingLicenseEntity>();
            Requests = new List<RequestEntity>();
        }

        public ChaffeurEntity()
        {
            ChaffeurVehicles = new List<ChaffeurEntityVehicleEntity>();
            DrivingLicenses = new List<DrivingLicenseEntity>();
            Requests = new List<RequestEntity>();
            ChaffeurFuelCards = new List<ChaffeurEntityFuelCardEntity>();
        }
    }
}
