using AutoMapper;
using DataLayer.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MappingTool : Profile
    {
        public MappingTool()
        {
            CreateMap<ChaffeurEntity, Chaffeur>();
        }
    }
}
