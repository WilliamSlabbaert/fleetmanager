using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class VehicleChaffeurEntity
    {
        public int VehicleEntityId { get; set; }
        public VehicleEntity Vehicle { get; set; }
        public int ChaffeurEntityId { get; set; }
        public ChaffeurEntity Chaffeur { get; set; }
        public bool IsActive { get; set; }
    }
}
