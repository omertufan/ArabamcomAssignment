using ArabamcomAssignment.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArabamcomAssignment.Repositories.Interfaces
{
    public interface IAdvertRepository
    {
        Task<(List<Advert> data, int totalCount)> GetAdverts(FilterDefinition<Advert> filter, SortDefinition<Advert> sorting, int page, int pageSize);
        Task<Advert> GetAdvert(int id);
        Task AddAdvertVisit(AdvertVisit visit);

    }
}
