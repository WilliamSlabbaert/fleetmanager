using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models.input
{
    public class FuelCardDTO
    {
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public DateTime ValidityDate { get; set; }
        public bool IsActive { get; set; }
    }
}
