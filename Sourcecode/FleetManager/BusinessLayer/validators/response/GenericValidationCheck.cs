using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators.response
{
    public static class GenericValidationCheck 
    {
        public static GenericResult<GeneralModels> CheckModel(FluentValidation.Results.ValidationResult validation, string errormessage)
        {
            var response = new GenericResult<GeneralModels>() { Message = "OK" };
            
            if (validation.IsValid == false)
            {
                response.SetStatusCode(Overall.ResponseType.BadRequest);
                response.Message = errormessage;
                response.ReturnValue = validation.Errors;
                return response;
            }
            return response;
        }
    }
}
