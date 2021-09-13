using Overall;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class FuelCardEntity
    {
        [Key]
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public int Pin { get; set; }
        public bool IsActive { get; set; }
        public List<AuthenticationTypeEntity> AuthenthicationCode { get; set; }
        public List<FuelEntity> FuelType { get; set; }
        public List<ExtraServiceEntity> Services { get; set; }
        public List<ChaffeurEntity> Chaffeurs { get; set; }
    }
}
