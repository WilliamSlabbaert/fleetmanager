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

namespace BusinessLayer.managers.interfaces
{
    public interface IDrivingLicenseService
    {
        public List<GenericResponse> _errors { get; set; }
        public GenericResult<IGeneralModels> GetAllDrivingLicenseById(int id);
        public GenericResult<IGeneralModels> GetAllDrivingLicenses();
        public GenericResult<IGeneralModels> GetAllDrivingLicensesPaging(GenericParameter parameters);
        public GenericResult<IGeneralModels> GetDrivingLicenseChaffeurById(int id);
        public GenericResult<IGeneralModels> AddDrivingLicense(DrivingLicenseDTO drivinglicense, int chaffeurid);
        public GenericResult<IGeneralModels> DeleteDrivingLicense(int drivinglicense, int chaffeurid);
        public object GetHeaders(GenericParameter parameters);
    }
}
