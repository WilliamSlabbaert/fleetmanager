using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models.input
{
    public class RepairmentDTO
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
    }
}
