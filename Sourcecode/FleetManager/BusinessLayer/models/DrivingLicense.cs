using BusinessLayer.models.general;
using Overall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class DrivingLicense : GeneralModels
    {
        public DrivingLicense(License type)
        {
            this.type = type;
        }
        public int Id { get; set; }

        public Overall.License type { get; set; }
        public int ChauffeurId { get; set; }
        public Chauffeur Chauffeur { get; set; }
    }
}
