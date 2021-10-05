using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators.response
{
    public class GenericResponse
    {
        public string Property { get; set; }
        public string Error { get; set; }
        public string Input { get; set; }
        //public string Type { get; set; }
    }
}
