using AutoShop.Models;

namespace AutoShop.Helpers
{
    public class PaginatedList<T>: List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            
            TotalPages = (int)Math.Ceiling((count / (double)pageSize));

            AddRange(items);
        }
        
        public bool HasPrevPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int count = source.Count();

            IQueryable<T> pagedItems = source.Skip((pageIndex - 1) * pageSize);
            IQueryable<T> selectedItems = pagedItems.Take(pageSize);
            List<T> items = selectedItems.ToList();

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}