using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class Request
    {
        public Request(DateTime startDate, DateTime endDate, string status)
        {
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int ChaffeurId { get; set; }
        public Chaffeur Chaffeur { get; set; }
        public List<Repairment> Repairment { get; set; }
        public List<Maintenance> Maintenance { get; set; }
    }
}
