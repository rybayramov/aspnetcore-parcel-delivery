using entity = PriceList.API.Entities;
using System.Threading.Tasks;

namespace PriceList.API.Repositories.Interfaces
{
    public interface IPriceListRepository
    {
        Task<entity.PriceList> GetPrice(string productWeight);

        Task<bool> CreatePrice(entity.PriceList PriceList);
        Task<bool> UpdatePrice(entity.PriceList PriceList);
    }
}
