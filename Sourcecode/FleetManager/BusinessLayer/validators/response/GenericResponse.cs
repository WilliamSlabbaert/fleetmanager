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
        public string Message { get; set; }
        public int StatusCode { get; private set; } = 400;
        public object ReturnValue { get; set; } = null;
    }
}
