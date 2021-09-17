using BusinessLayer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IChaffeurManager 
    {
        public Chaffeur GetChaffeurById(int id);
        public void AddChaffeur(Chaffeur ch);
        public void UpdateChaffeur(Chaffeur ch);
        public void AddVehicleToChaffeur(Chaffeur ch, Vehicle vh);
        public void RemoveVehicleToChaffeur(Chaffeur ch, Vehicle vh);
        public List<Chaffeur> GetAllChaffeurs();
    }
}
