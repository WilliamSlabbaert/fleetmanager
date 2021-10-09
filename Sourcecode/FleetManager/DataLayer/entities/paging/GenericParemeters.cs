using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities.paging
{
    public class GenericParemeters
    {
        const int maxPageSize = 5;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 4;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
