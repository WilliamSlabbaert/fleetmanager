﻿using BusinessLayer.models;
using BusinessLayer.validators.response;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IRepairmentService
    {
        public List<GenericResponse> _errors { get; set; }
        public GenericResult GetAllRepairments();
        public GenericResult GetRepairmentById(int id);
        public GenericResult GetRepairmentRequestById(int id);
        public GenericResult GetAllRepairmentsPaging(GenericParameter parameters);
        public Repairment AddRepairment(Repairment repairment, int requestId);
        public Repairment UpdateRepairment(Repairment repairment, int requestId, int repairmentId);
        
    }
}
