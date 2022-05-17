using Microsoft.EntityFrameworkCore;

namespace SD_125_BugTracker.Models
{
    public class PaginationList<T> : List<T>
    {
        public int PageIndex
        {
            get; private set;
        }
        public int TotalPages
        {
            get; private set;
        }

        public PaginationList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;                                        // current page index
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);     // total page count; Ceiling example: Math.Ceiling(11/10)=2; Math.Ceiling(-11/10)=-1;

            this.AddRange(items);         // add the items to PaginatedList
        }
        //check if the previousPage or NextPage button is avalabile
        public bool HasPreviousPage => PageIndex > 1;  //if pageIndex >1, HasPreviousPage = true, else false;

        public bool HasNextPage => PageIndex < TotalPages; //if HasNextPage < TotalPages, HasNextPage = true, else false;

        //Static method:get the List of items of current page
        public static async Task<PaginationList<T>> CreateAsync(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();//total items count
            var items = source.Skip(( pageIndex - 1 ) * pageSize).Take(pageSize).ToList(); //return a List containing only the requested page; eg: page 1 -> take(1-10) items
            return new PaginationList<T>(items, count, pageIndex, pageSize);//call the constructor method and return the List of items
        }

    }
}
