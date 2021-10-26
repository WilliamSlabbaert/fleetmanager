using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.validators.mediator
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : GenericResult<GeneralModels> 
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //pre

            var failures = _validators
                .Select(x => x.Validate(request))
                .SelectMany(s => s.Errors)
                .Where(s => s != null)
                .ToList();


            if (failures.Any())
            {
                var temp = new GenericResult<GeneralModels>() { Message= "Invalid input" };
                temp.ReturnValue = failures;
                temp.SetStatusCode(Overall.ResponseType.BadRequest);
                return (TResponse) temp;
                //throw new ValidationException(failures);
            }
            
            return await next();
            //post
        }
    }
}
