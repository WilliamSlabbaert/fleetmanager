﻿using BusinessLayer.models;
using BusinessLayer.validators.response;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IChaffeurService 
    {
        public List<GenericResponse> _errors { get; set; }
        public Chaffeur GetChaffeurById(int id);
        public Chaffeur AddChaffeur(Chaffeur ch);
        public bool checkExistingChaffeur(Chaffeur ch);
        public Chaffeur UpdateChaffeur(Chaffeur ch, int id);
        public void AddVehicleToChaffeur(int chnr, int vhnr);
        public List<Chaffeur> GetAllChaffeurs();
    }
}
