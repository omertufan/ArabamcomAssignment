using CsvHelper.Configuration.Attributes;
using System;

namespace ArabamcomAssignment.CsvFile
{
    public class AdvertCsv
    {
        [Index(0)]
        public int Id { get; set; }
        [Index(1)]
        public int MemberId { get; set; }
        [Index(2)]
        public int CityId { get; set; }
        [Index(3)]
        public string CityName { get; set; }
        [Index(4)]
        public int TownId { get; set; }
        [Index(5)]
        public string TownName { get; set; }
        [Index(6)]
        public int ModelId { get; set; }
        [Index(7)]
        public string ModelName { get; set; }
        [Index(8)]
        public int Year { get; set; }
        [Index(9)]
        public int Price { get; set; }
        [Index(10)]
        public string Title { get; set; }
        [Index(11)]
        public DateTime Date { get; set; }
        [Index(12)]
        public int CategoryId { get; set; }
        [Index(13)]
        public string Category { get; set; }
        [Index(14)]
        public int Km { get; set; }
        [Index(15)]
        public string Color { get; set; }
        [Index(16)]
        public string Gear { get; set; }
        [Index(17)]
        public string Fuel { get; set; }
        [Index(18)]
        public string FirstPhoto { get; set; }
        [Index(19)]
        public string SecondPhoto { get; set; }
        [Index(20)]
        public string userInfo { get; set; }
        [Index(21)]
        public string UserPhone { get; set; }
        [Index(22)]
        public string Text { get; set; }
    }
}
