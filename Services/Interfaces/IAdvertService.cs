using ArabamcomAssignment.Model;
using System.Threading.Tasks;

namespace ArabamcomAssignment.Services.Interfaces
{
    public interface IAdvertService
    {
        Task<AdvertPagingResponse> GetAdverts(AdvertPagingRequest request);
        Task<AdvertResponse> GetAdvert(int id);
        Task AddAdvertVisit(string advertId, string ipAddress);
    }
}
