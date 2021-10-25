﻿using BusinessLayer.mediator.queries;
using BusinessLayer.models.general;
using DataLayer.entities.generic;
using MediatR;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.handlers.queries
{
    public class GetHeadersHandler : IRequestHandler<GetHeadersQuery, PagedList<IGeneralWithIDEntities>>
    {
        public Task<PagedList<IGeneralWithIDEntities>> Handle(GetHeadersQuery request, CancellationToken cancellationToken)
        {
            var temp = request._list;
            var value = PagedList<IGeneralWithIDEntities>.ToPagedList(temp, request._parameter.PageNumber, request._parameter.PageSize);
            
            return Task.FromResult(value);
        }
    }
}
