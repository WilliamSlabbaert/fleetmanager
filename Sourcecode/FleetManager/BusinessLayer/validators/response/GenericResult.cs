using BusinessLayer.models;
using BusinessLayer.models.general;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators.response
{
    public class GenericResult<T> where T : GeneralModels
    {
        public string Message { get; set; }
        public int StatusCode { get; private set; } = 400;
        public object ReturnValue { get; set; } = null;
        public void SetStatusCode(Overall.ResponseType type)
        {
            var fieldInfo = type.GetType().GetField(type.ToString());
            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            this.StatusCode = Int32.Parse(descriptionAttributes[0].Description.ToString());
        }
    }
}
