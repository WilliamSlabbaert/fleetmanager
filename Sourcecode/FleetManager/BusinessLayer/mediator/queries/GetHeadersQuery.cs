using MediatR;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.queries
{
    public class GetHeadersQuery : IRequest<PagedList<object>>
    {
        public GetHeadersQuery(GenericParameter parameter, IQueryable<object> list)
        {
            _parameter = parameter;
            _list = list;
        }

        public GenericParameter _parameter { get; private set; }
        public IQueryable<object> _list { get; private set; }
    }
}
