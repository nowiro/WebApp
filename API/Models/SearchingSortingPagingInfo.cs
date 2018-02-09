using System.Collections.Generic;

namespace API.Models
{
    public class SearchingSortingPagingInfo  
    {
        public string Search { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int Page { get; set; }
        public int ResultCount { get; set; }
    }


    public class ResultsInfo
    {
        public List<Product> Data { get; set; }
        public SearchingSortingPagingInfo Info { get; set; }
    }
}  