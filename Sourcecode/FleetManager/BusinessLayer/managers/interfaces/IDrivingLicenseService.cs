﻿using BusinessLayer.models;
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
        public void AddDrivingLicense(DrivingLicense drivinglicense, int chaffeurid);
        public List<DrivingLicense> GetAllDrivingLicenses();
        public DrivingLicense GetAllDrivingLicenseById(int id);
    }
}