using BusinessLayer.services;
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
            var nr = "98.06.14-185.40";
            var countNR = Int32.Parse(nr.Remove(nr.Length - 3).Replace(".","").Replace("-",""));
            var result1 = countNR / 97;
            var result2 = result1 * 97;
            var result3 = countNR - result2;
            var controlDigit = 97 - result3;
            var finalNr = nr.Substring(nr.Length - 2);
            Console.WriteLine(finalNr);
        }
    }
}
