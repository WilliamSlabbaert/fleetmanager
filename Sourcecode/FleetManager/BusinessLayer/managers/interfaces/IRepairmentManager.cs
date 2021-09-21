using BusinessLayer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IRepairmentManager
    {
        public void AddRepairment(Repairment repairment, int requestId);
        public List<Repairment> GetAllRepairments();
        public Repairment GetRepairmentById(int id);
    }
}
