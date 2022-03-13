using ArabamcomAssignment.Data.Interfaces;
using ArabamcomAssignment.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ArabamcomAssignment.Data
{
    public class DbContext : IDbContext
    {
        public DbContext(IConfiguration configuration, IWebHostEnvironment env)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Adverts = database.GetCollection<Advert>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            DbContextSeed.SeedData(Adverts, env);

            List<CreateIndexModel<Advert>> indexModels = new List<CreateIndexModel<Advert>>();

            var indexKeysPrice = Builders<Advert>.IndexKeys.Ascending(indexKey => indexKey.Price);
            indexModels.Add(new CreateIndexModel<Advert>(indexKeysPrice));
            var indexKeysFuel = Builders<Advert>.IndexKeys.Ascending(indexKey => indexKey.Fuel);
            indexModels.Add(new CreateIndexModel<Advert>(indexKeysFuel));
            var indexKeysGear = Builders<Advert>.IndexKeys.Ascending(indexKey => indexKey.Gear);
            indexModels.Add(new CreateIndexModel<Advert>(indexKeysGear));
            var indexKeysYear = Builders<Advert>.IndexKeys.Ascending(indexKey => indexKey.Year);
            indexModels.Add(new CreateIndexModel<Advert>(indexKeysYear));
            var indexKeysKm = Builders<Advert>.IndexKeys.Ascending(indexKey => indexKey.Km);
            indexModels.Add(new CreateIndexModel<Advert>(indexKeysKm));
            var indexKeysAdvertId = Builders<Advert>.IndexKeys.Ascending(indexKey => indexKey.AdvertId);
            indexModels.Add(new CreateIndexModel<Advert>(indexKeysAdvertId));
            var indexKeysCategoryId = Builders<Advert>.IndexKeys.Ascending(indexKey => indexKey.CategoryId);
            indexModels.Add(new CreateIndexModel<Advert>(indexKeysCategoryId));

            Adverts.Indexes.CreateMany(indexModels);

            AdvertVisits = database.GetCollection<AdvertVisit>(configuration.GetValue<string>("DatabaseSettings:CollectionName2"));
        }

        public IMongoCollection<Advert> Adverts { get; }
        public IMongoCollection<AdvertVisit> AdvertVisits { get; }
    }
}
