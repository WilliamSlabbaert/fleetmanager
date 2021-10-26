using BusinessLayer.models.general;
using Overall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class AuthenticationType : GeneralModels
    {
        public AuthenticationType(AuthenticationTypes type)
        {
            this.type = type;
        }
        public int Id { get; set; }
        public AuthenticationTypes type { get; set; }
        public int FuelCardId { get; set; }
        public FuelCard FuelCard { get; set; }
    }
}
