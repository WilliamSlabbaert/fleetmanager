using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities.generic
{
    public interface IGeneralWithIDEntities
    {
        [Key]
        public int Id { get; set; }
    }
}
