using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ArabamcomAssignment.Entities
{
    public class AdvertVisit
    {
        [BsonId]
        public string Id { get; set; }
        public int AdvertId { get; set; }
        public string IPAdress { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime VisitDate { get; set; }
    }
}
