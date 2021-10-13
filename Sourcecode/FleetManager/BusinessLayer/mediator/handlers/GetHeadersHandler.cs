using BusinessLayer.mediator.queries;
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

namespace BusinessLayer.mediator.handlers
{
    public class GetHeadersHandler : IRequestHandler<GetHeadersQuery, PagedList<IGeneralEntities>>
    {
        public Task<PagedList<IGeneralEntities>> Handle(GetHeadersQuery request, CancellationToken cancellationToken)
        {
            var temp = request._list;
            var value = PagedList<IGeneralEntities>.ToPagedList(temp, request._parameter.PageNumber, request._parameter.PageSize);
            
            return Task.FromResult(value);
        }
    }
}
