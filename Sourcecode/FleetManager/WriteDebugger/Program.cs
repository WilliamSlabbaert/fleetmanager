using DataLayer;
using DataLayer.entities;
using DataLayer.repositories;
using System;
using System.Linq;

namespace WriteDebugger
{
    class Program
    {
        static void Main(string[] args)
        {
            var temp = new FleetManagerContext();
            GenericRepo<ChaffeurEntity> rep = new GenericRepo<ChaffeurEntity>(temp);
            //rep.AddEntity(new ChaffeurEntity("test","test2","test3","test4","test5", DateTime.Now,"test6",true));

            rep.Save();

        }
    }
}
