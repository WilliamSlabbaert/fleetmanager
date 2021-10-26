using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.models.input;
using BusinessLayer.validators.response;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.services.interfaces
{
    public interface IDrivingLicenseService
    {
        public GenericResult<GeneralModels> GetAllDrivingLicenseById(int id);
        public GenericResult<GeneralModels> GetAllDrivingLicenses();
        public GenericResult<GeneralModels> GetAllDrivingLicensesPaging(GenericParameter parameters);
        public GenericResult<GeneralModels> GetDrivingLicenseChaffeurById(int id);
        public GenericResult<GeneralModels> AddDrivingLicense(DrivingLicenseDTO drivinglicense, int chaffeurid);
        public GenericResult<GeneralModels> DeleteDrivingLicense(int drivinglicense, int chaffeurid);
        public object GetHeaders(GenericParameter parameters);
    }
}
