using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ArabamcomAssignment.Entities
{
    public class Advert
    {
        [BsonId]
        public string Id { get; set; }
        public int AdvertId { get; set; }
        public int MemberId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int TownId { get; set; }
        public string TownName { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public string Title { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int Km { get; set; }
        public string Color { get; set; }
        public string Gear { get; set; }
        public string Fuel { get; set; }
        public string FirstPhoto { get; set; }
        public string SecondPhoto { get; set; }
        public string userInfo { get; set; }
        public string UserPhone { get; set; }
        public string Text { get; set; }

    }
}
