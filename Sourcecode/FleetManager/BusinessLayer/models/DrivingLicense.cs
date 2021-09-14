using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class DrivingLicense
    {
        public int Id { get; set; }
        public Overall.License type { get; set; }
        public int ChaffeurId { get; set; }
        public Chaffeur Chaffeur { get; set; }
    }
}
