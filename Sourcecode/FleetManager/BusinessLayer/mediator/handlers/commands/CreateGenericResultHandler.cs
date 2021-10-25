using BusinessLayer.mediator.commands;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.handlers
{
    public class CreateGenericResultHandler : IRequestHandler<CreateGenericResultCommand, GenericResult<GeneralModels>>
    {
        public Task<GenericResult<GeneralModels>> Handle(CreateGenericResultCommand request, CancellationToken cancellationToken)
        {
            var resp = new GenericResult<GeneralModels>()
            {
                Message = request.Message,
                ReturnValue = request.Value
            };
            resp.SetStatusCode(request.Type);
            return Task.FromResult(resp);
        }
    }
}
