﻿using AutoMapper;
using BusinessLayer.mediator.commands;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
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
    public class AddKilometerHistoryHandler : IRequestHandler<AddKilometerHistoryCommand, GenericResult<IGeneralModels>>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddKilometerHistoryHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IMediator mediator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._mediator = mediator;
        }
        public Task<GenericResult<IGeneralModels>> Handle(AddKilometerHistoryCommand request, CancellationToken cancellationToken)
        {
            var temp = _vehicleRepo.GetById(s=>s.Id == request._vehicleId,s=>s.Include(s=> s.Kilometers));
            var dto = _mapper.Map<KilometerHistory>(request.kilometer);
            temp.Kilometers.Add(_mapper.Map<KilometerHistoryEntity>(dto));
            _vehicleRepo.UpdateEntity(temp);
            _vehicleRepo.Save();
            var response = new GenericResult<IGeneralModels>() { Message = "Ok", ReturnValue = temp};
            response.SetStatusCode(Overall.ResponseType.OK);
            return Task.FromResult(response);
        }
    }
}
