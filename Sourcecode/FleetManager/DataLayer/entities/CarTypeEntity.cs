using Overall;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class CarTypeEntity
    {
        [Key]
        public int Id { get; set; }
        public CarTypes Type { get; set; }
        public int VehicleId { get; set; }
        public VehicleEntity Vehicle { get; set; }
    }
}
