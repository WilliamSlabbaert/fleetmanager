﻿using BusinessLayer.models;
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
    public interface IRequestService
    {
        public object GetHeaders(GenericParameter parameters);
        public GenericResult<IGeneralModels> GetRequestById(int id);
        public GenericResult<IGeneralModels> GetRequestChaffeur(int id);
        public GenericResult<IGeneralModels> GetRequestVehicle(int id);
        public GenericResult<IGeneralModels> GetRequestRepairs(int id);
        public GenericResult<IGeneralModels> GetRequestMaintenance(int id);
        public GenericResult<IGeneralModels> GetAllRequests();
        public GenericResult<IGeneralModels> GetAllRequestsPaging(GenericParameter parameters);
        public GenericResult<IGeneralModels> AddRequest(RequestDTO request, int chaffeurId, int vehicleId);
        public GenericResult<IGeneralModels> UpdateRequest(RequestDTO request, int id);
    }
}
