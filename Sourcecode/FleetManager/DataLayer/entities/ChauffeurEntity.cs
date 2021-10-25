using DataLayer.entities.generic;
using Overall;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class ChauffeurEntity : IGeneralWithIDEntities
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalInsurenceNumber { get; set; }
        public bool IsActive { get; set; }
        public List<ChauffeurEntityVehicleEntity> ChauffeurVehicles { get; set; }
        public List<ChauffeurEntityFuelCardEntity> ChauffeurFuelCards { get; set; }
        public List<DrivingLicenseEntity> DrivingLicenses { get; set; }
        public List<RequestEntity> Requests { get; set; }
        public ChauffeurEntity(string firstName, string lastName, string city, string street, string houseNumber, DateTime dateOfBirth, string nationalInsurenceNumber, bool isActive)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            DateOfBirth = dateOfBirth;
            NationalInsurenceNumber = nationalInsurenceNumber;
            IsActive = isActive;
            ChauffeurVehicles = new List<ChauffeurEntityVehicleEntity>();
            ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>();
            DrivingLicenses = new List<DrivingLicenseEntity>();
            Requests = new List<RequestEntity>();
        }

        public ChauffeurEntity()
        {
            ChauffeurVehicles = new List<ChauffeurEntityVehicleEntity>();
            DrivingLicenses = new List<DrivingLicenseEntity>();
            Requests = new List<RequestEntity>();
            ChauffeurFuelCards = new List<ChauffeurEntityFuelCardEntity>();
        }
    }
}
