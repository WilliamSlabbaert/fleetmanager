using AutoMapper;
using BusinessLayer.mediator.commands;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.handlers.commands
{
    public class AddKilometerHistoryHandler : IRequestHandler<AddKilometerHistoryCommand, GenericResult<GeneralModels>>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private IValidator<KilometerHistory> _kilometerValidator;
        private readonly IMediator _mediator;

        public AddKilometerHistoryHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IMediator mediator,KilometerHistoryValidator kilometerV)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._mediator = mediator;
            this._kilometerValidator = kilometerV;
        }
        public Task<GenericResult<GeneralModels>> Handle(AddKilometerHistoryCommand request, CancellationToken cancellationToken)
        {
            var temp = _vehicleRepo.GetById(s=>s.Id == request._vehicleId,s=>s.Include(s=> s.Kilometers));
            var kilometerHistory = _mapper.Map<KilometerHistory>(request.kilometer);
            var check = _kilometerValidator.Validate(kilometerHistory);
            var result = GenericValidationCheck.CheckModel(check, "Kilometer history is invalid.");
            if (check.IsValid)
            {
                temp.Kilometers.Add(_mapper.Map<KilometerHistoryEntity>(kilometerHistory));
                _vehicleRepo.UpdateEntity(temp);
                _vehicleRepo.Save();
                var response = new GenericResult<GeneralModels>() { Message = "Ok", ReturnValue = temp };
                response.SetStatusCode(Overall.ResponseType.OK);
                return Task.FromResult(response);
            }
            return Task.FromResult(result);
        }
    }
}
