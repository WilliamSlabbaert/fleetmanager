using BusinessLayer.models.general;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class Repairment : IGeneralModels
    {
        public Repairment(DateTime date, string description, string company)
        {
            Date = date;
            Description = description;
            Company = company;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }
    }
}
