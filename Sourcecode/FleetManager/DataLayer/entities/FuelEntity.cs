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
    public class FuelEntity : IGeneralEntities
    {
        [Key]
        public int Id { get; set; }
        public FuelTypes Fuel { get; set; }
        public int FuelCardId { get; set; }
        public FuelCardEntity FuelCard { get; set; }
    }
}
