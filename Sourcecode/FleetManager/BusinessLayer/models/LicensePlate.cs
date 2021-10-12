using BusinessLayer.models.general;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class LicensePlate : IGeneralModels
    {
        public LicensePlate(string plate, bool active = false)
        {
            this.Plate = plate.ToUpper();
            this.IsActive = active;
        }

        public int Id { get; set; }
        public string Plate { get; set; }
        public bool IsActive { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
