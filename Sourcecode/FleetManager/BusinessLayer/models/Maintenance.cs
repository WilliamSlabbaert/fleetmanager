﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class Maintenance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public string Garage { get; set; }
        public List<Invoice> Invoices { get; set; }
        public Request Request { get; set; }
    }
}