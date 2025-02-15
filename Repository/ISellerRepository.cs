using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Repositories
{
    public interface ISellerRepository
    {

        Task<IEnumerable<Seller>> Getall();
     
        Task<Seller> GetDetails(int Sellerid);
        Task<Seller> GetDetailsbyEmail(string email);

        Task<bool> UpdateSeller(Seller seller);
    }
}
