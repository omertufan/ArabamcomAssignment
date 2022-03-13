using ArabamcomAssignment.CsvFile;
using ArabamcomAssignment.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ArabamcomAssignment.Data.Interfaces
{
    public class DbContextSeed
    {
        public static void SeedData(IMongoCollection<Advert> advertCollection, IWebHostEnvironment env)
        {
            bool existAdvert = advertCollection.Find(p => true).Any();
            if (!existAdvert)
            {
                advertCollection.InsertManyAsync(GetPreconfiguredAdverts(env));
            }
        }

        private static IEnumerable<Advert> GetPreconfiguredAdverts(IWebHostEnvironment env)
        {

            var records = new List<AdvertCsv>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };

            using (var reader = new StreamReader(env.ContentRootPath + "/CsvFile/Adverts.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                records = csv.GetRecords<AdvertCsv>().ToList();
            }

            List<Advert> data = new List<Advert>();

            foreach (var item in records)
            {
                data.Add(new Advert
                {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    SecondPhoto = item.SecondPhoto,
                    Category = item.Category,
                    CityName = item.CityName,
                    CategoryId = item.CategoryId,
                    CityId = item.CityId,
                    Color = item.Color,
                    Date = item.Date,
                    FirstPhoto = item.FirstPhoto,
                    Gear = item.Gear,
                    Fuel = item.Fuel,
                    AdvertId = item.Id,
                    Km = item.Km,
                    MemberId = item.MemberId,
                    ModelId = item.ModelId,
                    ModelName = item.ModelName,
                    Price = item.Price,
                    Text = item.Text,
                    Title = item.Title,
                    TownId = item.TownId,
                    TownName = item.TownName,
                    userInfo = item.userInfo,
                    UserPhone = item.UserPhone,
                    Year = item.Year,

                });
            }


            return data;
        }
    }
}
