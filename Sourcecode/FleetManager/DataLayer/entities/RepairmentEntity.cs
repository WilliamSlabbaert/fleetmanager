using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class RepairmentEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public int RequestId { get; set; }
        public RequestEntity Request { get; set; }
    }
}
