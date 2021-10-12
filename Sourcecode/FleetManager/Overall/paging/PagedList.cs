using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overall.paging
{
    public class PagedList<T> : List<T>
    {
        public PagedList(List<T> items,int totalCount, int pageNumber, int pageSize)
        {
            this.CurrentPage = pageNumber;
            this.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            this.PageSize = pageSize;
            this.TotalCount = totalCount;

            AddRange(items);
        }
        public static PagedList<T> ToPagedList(IQueryable<T> source, int pagenumber, int pagesize)
        {
            var count = source.Count();
            var items = source.Skip((pagenumber - 1) * pagesize)
                .Take(pagesize)
                .ToList();

            return new PagedList<T>(items,count, pagenumber, pagesize);
        }

        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
