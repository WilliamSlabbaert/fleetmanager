using DataLayer.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class FuelCard
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public int Pin { get; set; }
        public List<AuthenticationTypes> AuthenthicationCode { get; set; }
        public List<FuelTypes> FuelType { get; set; }
        public List<ExtraServices> Services { get; set; }
        public bool IsActive { get; set; }
    }
}
