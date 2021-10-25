using BusinessLayer.models.general;
using DataLayer.entities.generic;
using MediatR;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.queries
{
    public class GetHeadersQuery : IRequest<PagedList<GeneralEntities>>
    {
        public GetHeadersQuery(GenericParameter parameter, IQueryable<GeneralEntities> list)
        {
            _parameter = parameter;
            _list = list;
        }

        public GenericParameter _parameter { get; private set; }
        public IQueryable<GeneralEntities> _list { get; private set; }
    }
}
