using Overall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class FuelType
    {
        public FuelType(FuelTypes fuel)
        {
            Fuel = fuel;
        }

        public int Id { get; set; }
        public FuelTypes Fuel { get; set; }
        public int FuelCardId { get; set; }
        public FuelCard FuelCard { get; set; }
    }
}
