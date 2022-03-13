using System.Collections.Generic;

namespace ArabamcomAssignment.Model
{
    public class AdvertPagingRequest
    {
        public int? categoryId { get; set; }
        public int? minPrice { get; set; }
        public int? maxPrice { get; set; }
        public List<string> Gear { get; set; }
        public List<string> Fuel { get; set; }
        public int Page { get; set; }
        public SortType? SortBy { get; set; }
    }

    public enum SortType
    {

        PriceAsc,
        PriceDesc,
        YearAsc,
        YearDesc,
        KmAsc,
        KmDesc
    }
}
