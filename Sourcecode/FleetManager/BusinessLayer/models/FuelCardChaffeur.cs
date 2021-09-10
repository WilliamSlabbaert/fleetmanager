﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class FuelCardChaffeur
    {
        public int ChaffeurId { get; set; }
        public Chaffeur Chaffeur { get; set; }

        public int FuelCardId { get; set; }
        public FuelCard FuelCard { get; set; }
        public bool IsActive { get; set; }
    }
}