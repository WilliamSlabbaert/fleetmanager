using BusinessLayer.models;
using BusinessLayer.validators.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IDrivingLicenseService
    {
        public List<GenericResponse> _errors { get; set; }
        public DrivingLicense AddDrivingLicense(DrivingLicense drivinglicense, int chaffeurid);
        public List<DrivingLicense> GetAllDrivingLicenses();
        public Chaffeur DeleteDrivingLicense(int drivinglicense, int chaffeurid);
        public bool CheckValidationDrivingLicense(DrivingLicense drivinglicense);
        public DrivingLicense GetAllDrivingLicenseById(int id);
        public bool CheckExistingDrivingLicense(int id, DrivingLicense license);
    }
}
