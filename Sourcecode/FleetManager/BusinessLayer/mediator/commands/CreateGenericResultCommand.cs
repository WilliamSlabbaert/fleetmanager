using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using MediatR;
using Overall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.commands
{
    public class CreateGenericResultCommand : IRequest<GenericResult<GeneralModels>>
    {
        public CreateGenericResultCommand(string message, ResponseType type, object value)
        {
            Message = message;
            Type = type;
            Value = value;
        }

        public string Message { get; private set; }
        public Overall.ResponseType Type { get; private set; }
        public object Value { get; private set; }
    }
}
