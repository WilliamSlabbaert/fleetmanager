using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class FuelCardChaffeurEntity
    {
        public int ChaffeurId { get; set; }
        public ChaffeurEntity Chaffeur { get; set; }

        public int FuelCardId { get; set; }
        public FuelCardEntity FuelCard { get; set; }
        public bool IsActive { get; set; }
    }
}
