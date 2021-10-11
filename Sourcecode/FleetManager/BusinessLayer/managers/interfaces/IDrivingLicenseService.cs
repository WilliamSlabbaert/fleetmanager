using BusinessLayer.models;
using BusinessLayer.validators.response;
using Overall.paging;
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
        public GenericResult GetAllDrivingLicenseById(int id);
        public GenericResult GetAllDrivingLicenses();
        public GenericResult GetAllDrivingLicensesPaging(GenericParameter parameters);
        public GenericResult GetDrivingLicenseChaffeurById(int id);
        public DrivingLicense AddDrivingLicense(DrivingLicense drivinglicense, int chaffeurid);
        public Chaffeur DeleteDrivingLicense(int drivinglicense, int chaffeurid);
        public bool CheckValidationDrivingLicense(DrivingLicense drivinglicense);
        public bool CheckExistingDrivingLicense(int id, DrivingLicense license);
    }
}
