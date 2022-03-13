using ArabamcomAssignment.Entities;
using ArabamcomAssignment.Data.Interfaces;
using ArabamcomAssignment.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ArabamcomAssignment.Repositories
{
    public class AdvertRepository : IAdvertRepository
    {
        private readonly IDbContext _context;

        public AdvertRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Advert> GetAdvert(int id)
        {
            return await _context
                        .Adverts
                        .Find(p => p.AdvertId == id)
                        .FirstOrDefaultAsync();
        }

        public async Task<(List<Advert> data, int totalCount)> GetAdverts(FilterDefinition<Advert> filter, SortDefinition<Advert> sorting, int page, int pageSize)
        {

            var data = await _context
                          .Adverts
                          .Find(filter).Sort(sorting)
                         .Skip((page - 1) * pageSize)
                         .Limit(pageSize).ToListAsync();

            var totalCount = (int) await _context.Adverts.CountDocumentsAsync(filter);

            return (data, totalCount);

        }

        public async Task AddAdvertVisit(AdvertVisit visit)
        {
            await _context.AdvertVisits.InsertOneAsync(visit);
        }
    }
}
