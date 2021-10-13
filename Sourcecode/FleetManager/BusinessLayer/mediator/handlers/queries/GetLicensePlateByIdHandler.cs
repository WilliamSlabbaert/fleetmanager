using AutoMapper;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.handlers.queries
{
    public class GetLicensePlateByIdHandler : IRequestHandler<GetLicensePlateByIdQuery, GenericResult<IGeneralModels>>
    {
        private readonly IGenericRepo<LicensePlateEntity> _repo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        public GetLicensePlateByIdHandler(IGenericRepo<LicensePlateEntity> repo, IMapper mapper, IMediator mediator)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._mediator = mediator;
        }
        public Task<GenericResult<IGeneralModels>> Handle(GetLicensePlateByIdQuery request, CancellationToken cancellationToken)
        {
            var temp = _repo.GetById(s => s.Id == request.licensePlateId,null);
            var respond = new GenericResult<IGeneralModels>() { Message = "Licenseplate('s) not found." };
            respond.SetStatusCode(Overall.ResponseType.NotFound);

            if(temp == null)
            {
                return Task.FromResult(respond);
            }

            respond.SetStatusCode(Overall.ResponseType.OK);
            respond.ReturnValue = _mapper.Map<LicensePlate>(temp);
            respond.Message = "Ok";

            return Task.FromResult(respond);
        }
    }
}
