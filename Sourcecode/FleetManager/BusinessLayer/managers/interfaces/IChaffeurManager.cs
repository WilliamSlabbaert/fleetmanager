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
        public string test();
    }
}
