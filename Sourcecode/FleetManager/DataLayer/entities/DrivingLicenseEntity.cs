using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class DrivingLicenseEntity
    {
        [Key]
        public int Id { get; set; }
        public Overall.License type { get; set; }
        public int ChaffeurId { get; set; }
        public ChaffeurEntity Chaffeur { get; set; }
    }
}
