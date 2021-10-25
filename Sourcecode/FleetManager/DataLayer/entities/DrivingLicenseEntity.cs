using DataLayer.entities.generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class DrivingLicenseEntity : GeneralEntities
    {
        public Overall.License type { get; set; }
        public int ChauffeurId { get; set; }
        public ChauffeurEntity Chauffeur { get; set; }
    }
}
