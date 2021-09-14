using BusinessLayer.managers;
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
            //var temp = new FleetManagerContext();
            //GenericRepo<ChaffeurEntity> rep = new GenericRepo<ChaffeurEntity>(temp);
            //rep.AddEntity(new ChaffeurEntity("test","test2","test3","test4","test5", DateTime.Now,"test6",true));
            //ChaffeurEntity test = rep.GetById(1);
            //test.FirstName = "William";
            //rep.UpdateEntity(test);

            //GenericRepo<VehicleEntity> rep2 = new GenericRepo<VehicleEntity>(temp);
            //rep2.AddEntity(new VehicleEntity(50,Overall.CarTypes.Passengercar,500.69));
            //VehicleEntity test2 = rep2.GetById(1);
            //test.RemoveVehicle(test2);
            /*test2.Chaffeurs.Add(test);
            rep2.UpdateEntity(test2);*/



            /*
            ChaffeurManager testManager = new ChaffeurManager(rep);
            var temp2 = testManager.GetChaffeurById(1);
            Console.WriteLine(temp2.GetType());
            Console.WriteLine(temp2.FirstName);
            Console.WriteLine(temp2.LastName);
            Console.WriteLine(temp2.City);
            Console.WriteLine(temp2.Street);
            Console.WriteLine(temp2.HouseNumber);
            Console.WriteLine(temp2.DateOfBirth);
            Console.WriteLine(temp2.NationalInsurenceNumber);
            Console.WriteLine(temp2.IsActive);
            rep.Save();*/

        }
    }
}
