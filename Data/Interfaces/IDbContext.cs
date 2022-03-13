using ArabamcomAssignment.Entities;
using MongoDB.Driver;

namespace ArabamcomAssignment.Data.Interfaces
{
    public interface IDbContext
    {
        IMongoCollection<Advert> Adverts { get; }
        IMongoCollection<AdvertVisit> AdvertVisits { get; }
    }
}
