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
    public class GetHeadersQuery : IRequest<PagedList<IGeneralWithIDEntities>>
    {
        public GetHeadersQuery(GenericParameter parameter, IQueryable<IGeneralWithIDEntities> list)
        {
            _parameter = parameter;
            _list = list;
        }

        public GenericParameter _parameter { get; private set; }
        public IQueryable<IGeneralWithIDEntities> _list { get; private set; }
    }
}
