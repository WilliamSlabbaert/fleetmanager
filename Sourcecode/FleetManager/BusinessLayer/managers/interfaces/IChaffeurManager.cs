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
        public void AddVehicleToChaffeur(int chnr, int vhnr);
        public void RemoveVehicleToChaffeur(int chnr, int vhnr);
        public List<Chaffeur> GetAllChaffeurs();
        public List<string> test(Chaffeur ch);
    }
}
