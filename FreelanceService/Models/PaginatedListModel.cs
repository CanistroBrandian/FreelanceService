using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FreelanceService.Web.Models
{
    public class PaginatedListModel<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedListModel(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedListModel<T>> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count =  source.Count();
            var items =  source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedListModel<T>(items, count, pageIndex, pageSize);
        }

        public static IEnumerable<int> PageSize()
        {
            List<int> page = new List<int>() { 5, 15, 25 };
            return page;
        }
    }
}
