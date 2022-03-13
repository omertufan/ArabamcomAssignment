using System.Collections.Generic;

namespace ArabamcomAssignment.Model
{
    public class AdvertPagingResponse
    {
        public AdvertPagingResponse()
        {
            Adverts = new List<AdvertDTO>();
        }
        public int Total { get; set; }
        public int Page { get; set; }
        public List<AdvertDTO> Adverts { get; set; }
    }

    public class AdvertDTO
    {

        public string Id { get; set; }
        public string ModelName { get; set; }
        public string Category { get; set; }
        public string Year { get; set; }
        public string Price { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Km { get; set; }
        public string Color { get; set; }
        public string Gear { get; set; }
        public string Fuel { get; set; }
        public string FirstPhoto { get; set; }

    }
}
