using DataLayer.entities.generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class LicensePlateEntity : IGeneralWithIDEntities
    {
        [Key]
        public int Id { get; set; }
        public string Plate { get; set; }
        public bool IsActive { get; set; }
        public int VehicleId { get; set; }
        public VehicleEntity Vehicle { get; set; }
    }
}
