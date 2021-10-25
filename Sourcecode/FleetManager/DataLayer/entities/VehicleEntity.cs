using DataLayer.entities.generic;
using Overall;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class VehicleEntity : GeneralEntities
    {
        public VehicleEntity()
        {
            Requests = new List<RequestEntity>();
            LicensePlates = new List<LicensePlateEntity>();
            ChauffeurVehicles = new List<ChauffeurEntityVehicleEntity>();
            Kilometers = new List<KilometerHistoryEntity>();
        }

        public int Chassis { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime BuildDate { get; set; }
        public CarTypes Type { get; set; }
        public FuelTypes FuelType { get; set; }
        public List<KilometerHistoryEntity> Kilometers { get; set; }
        public List<ChauffeurEntityVehicleEntity> ChauffeurVehicles{ get; set; }
        public List<RequestEntity> Requests { get; set; }
        public List<LicensePlateEntity> LicensePlates { get; set; }
    }
}
