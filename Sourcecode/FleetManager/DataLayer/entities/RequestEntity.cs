﻿using DataLayer.entities.generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class RequestEntity : GeneralEntities
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public Overall.RequestType Type { get; set; }
        public int VehicleId { get; set; }
        public VehicleEntity Vehicle { get; set; }
        public int ChauffeurId { get; set; }
        public ChauffeurEntity Chauffeur { get; set; }
        public List<RepairmentEntity> Repairment { get; set; }
        public List<MaintenanceEntity> Maintenance { get; set; }
    }
}
