using ArabamcomAssignment.Entities;
using ArabamcomAssignment.Model;
using ArabamcomAssignment.Repositories.Interfaces;
using ArabamcomAssignment.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArabamcomAssignment.Services
{
    public class AdvertService : IAdvertService
    {

        private readonly IAdvertRepository _repository;
        private readonly IConfiguration _configuration;

        public AdvertService(IAdvertRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task AddAdvertVisit(string advertId, string ipAddress)
        {
            AdvertVisit model = new AdvertVisit()
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                AdvertId = int.Parse(advertId),
                VisitDate = DateTime.Now,
                IPAdress = ipAddress

            };

            await Task.Run(() =>
            {

                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(_configuration.GetValue<string>("RabbitMQSettings:HostAddress"))
                };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: "advertVisit",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        string message = JsonSerializer.Serialize(model);
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(
                            exchange: "",
                            routingKey: "advertVisit",
                            basicProperties: null,
                            body: body);

                    }
                }
            });
        }

        public async Task<AdvertResponse> GetAdvert(int id)
        {
            AdvertResponse response = null;

            var result = await _repository.GetAdvert(id);

            if (result != null)
            {
                response = new AdvertResponse
                {
                    SecondPhoto = result.SecondPhoto,
                    Category = result.Category,
                    CityName = result.CityName,
                    CategoryId = result.CategoryId.ToString(),
                    CityId = result.CityId.ToString(),
                    Color = result.Color,
                    Date = result.Date.ToString(),
                    FirstPhoto = result.FirstPhoto,
                    Gear = result.Gear,
                    Fuel = result.Fuel,
                    Id = result.AdvertId.ToString(),
                    Km = result.Km.ToString(),
                    MemberId = result.MemberId.ToString(),
                    ModelId = result.ModelId.ToString(),
                    ModelName = result.ModelName,
                    Price = result.Price.ToString(),
                    Text = result.Text,
                    Title = result.Title,
                    TownId = result.TownId.ToString(),
                    TownName = result.TownName,
                    userInfo = result.userInfo,
                    UserPhone = result.UserPhone,
                    Year = result.Year.ToString(),

                };
            }


            return response;
        }

        public async Task<AdvertPagingResponse> GetAdverts(AdvertPagingRequest request)
        {
            AdvertPagingResponse response = new AdvertPagingResponse();

            int pageSize = 100;
            int page = 1;

            if (request.Page != 0)
            {
                page = request.Page;
            }


            var builder = Builders<Advert>.Filter;
            var filter = builder.Empty;

            if (request.categoryId != null && request.categoryId.HasValue)
            {
                var categoryFilter = builder.Eq(x => x.CategoryId, request.categoryId);
                filter &= categoryFilter;
            }

            if (request.minPrice != null && request.minPrice.HasValue)
            {
                var minPriceFilter = builder.Gt(x => x.Price, request.minPrice);
                filter &= minPriceFilter;
            }

            if (request.maxPrice != null && request.maxPrice.HasValue)
            {
                var maxPriceFilter = builder.Lt(x => x.Price, request.maxPrice);
                filter &= maxPriceFilter;
            }

            if (request.Fuel != null)
            {
                var fuelFilter = builder.In("Fuel", request.Fuel.ToArray());
                filter &= fuelFilter;
            }

            if (request.Gear != null)
            {
                var gearFilter = builder.In("Gear", request.Gear.ToArray());
                filter &= gearFilter;
            }

            var builderSort = Builders<Advert>.Sort;
            var sorting = builderSort.Ascending("Price");


            if (request.SortBy != null && request.SortBy.HasValue)
            {
                switch (request.SortBy)
                {
                    case SortType.PriceAsc:
                        sorting = builderSort.Ascending("Price");
                        break;
                    case SortType.PriceDesc:
                        sorting = builderSort.Descending("Price");
                        break;
                    case SortType.YearAsc:
                        sorting = builderSort.Ascending("Year");
                        break;
                    case SortType.YearDesc:
                        sorting = builderSort.Descending("Year");
                        break;
                    case SortType.KmAsc:
                        sorting = builderSort.Ascending("Km");
                        break;
                    case SortType.KmDesc:
                        sorting = builderSort.Descending("Km");
                        break;
                    default:
                        sorting = builderSort.Ascending("Price");
                        break;
                }
            }

            var result = await _repository.GetAdverts(filter, sorting, page, pageSize);

            foreach (var item in result.data)
            {
                response.Adverts.Add(new AdvertDTO
                {
                    Category = item.Category,
                    Color = item.Color,
                    Date = item.Date.ToString(),
                    FirstPhoto = item.FirstPhoto,
                    Fuel = item.Fuel,
                    Gear = item.Gear,
                    Id = item.AdvertId.ToString(),
                    Km = item.Km.ToString(),
                    ModelName = item.ModelName,
                    Price = item.Price.ToString(),
                    Title = item.Title,
                    Year = item.Year.ToString()

                });
            }


            response.Total = result.totalCount;
            response.Page = page;

            return response;

        }
    }
}
