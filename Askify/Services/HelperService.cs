using Askify.Services.IServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Askify.Services
{
    public class HelperService
    {
        public static List<int> Paginate(int page, int pageSize, int numberOfPages, int TotalCount)
        {
            double _total = TotalCount;
            double _pageSize = pageSize;
            if (Math.Ceiling(_total/ _pageSize) <numberOfPages)
                numberOfPages = (int) Math.Ceiling(_total / _pageSize);
            
            List<int> pages = new List<int>();   
            int start = 1;
            if (page - numberOfPages > 0)
                start = page - numberOfPages + 1;

            while (numberOfPages > 0)
            {
                pages.Add(start++);
                numberOfPages--;
            }
            return pages;
        }
        public static int GetPageSize() => 10;
        public static int GetNumberOfPages() => 5;
    }
}
