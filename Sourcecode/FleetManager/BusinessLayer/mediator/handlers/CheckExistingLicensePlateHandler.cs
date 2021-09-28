using AutoMapper;
using BusinessLayer.mediator.queries;
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

namespace BusinessLayer.mediator.handlers
{
    public class CheckExistingLicensePlateHandler : IRequestHandler<CheckExistingLicensePlateQuery, bool>
    {
        private readonly IGenericRepo<LicensePlateEntity> _licensePlateRepo;
        private readonly IMapper _mapper;
        public CheckExistingLicensePlateHandler(IGenericRepo<LicensePlateEntity> licensePlateRepo, IMapper mapper)
        {
            this._licensePlateRepo = licensePlateRepo;
            this._mapper = mapper;
        }
        public Task<bool> Handle(CheckExistingLicensePlateQuery request, CancellationToken cancellationToken)
        {
            var temp = _licensePlateRepo.GetAll(null).FirstOrDefault(s => s.Plate.Equals(request.licensePlate.Plate));
            if(temp == null)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
