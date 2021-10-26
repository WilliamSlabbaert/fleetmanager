using BusinessLayer.models.general;
using Overall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class ExtraService : GeneralModels
    {
        public ExtraService(ExtraServices service)
        {
            Service = service;
        }
        public int Id { get; set; }

        public ExtraServices Service { get; set; }
        public int FuelCardId { get; set; }
        public FuelCard FuelCard { get; set; }
    }
}
