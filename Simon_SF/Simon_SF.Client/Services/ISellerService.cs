using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Shared;

namespace Blazor.SIMONStore.Client.Services
{
    public interface ISellerService
    {
        Task<IEnumerable<Seller>> GetAll();
        Task<Seller> GetByid(int id);
        Task<Seller> GetByEmail(string Email);
        Task SaveSeller(Seller seller);
    }
}
