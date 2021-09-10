﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class RequestEntity
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int VehicleId { get; set; }
        public VehicleEntity Vehicle { get; set; }
        public int ChaffeurId { get; set; }
        public ChaffeurEntity Chaffeur { get; set; }
        public int RepairmentId { get; set; }
        public RepairmentEntity Repairment { get; set; }
        public int MaintenanceId { get; set; }
        public MaintenanceEntity Maintenance { get; set; }
        
    }
}