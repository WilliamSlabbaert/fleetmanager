using Overall;
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
        public bool IsActive { get; set; }
        public List<AuthenticationType> AuthenthicationCode { get; set; }
        public List<FuelType> FuelType { get; set; }
        public List<ExtraService> Services { get; set; }
        public List<Chaffeur> Chaffeurs { get; set; }
    }
}
