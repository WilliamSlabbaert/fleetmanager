﻿using BusinessLayer.models.general;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class Request : GeneralModels
    {
        public Request(DateTime startDate, DateTime endDate, string status, Overall.RequestType type)
        {
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            Type = type;
        }
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public Overall.RequestType Type { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int ChauffeurId { get; set; }
        public Chauffeur Chauffeur { get; set; }
        public List<Repairment> Repairment { get; set; }
        public List<Maintenance> Maintenance { get; set; }
    }
}
