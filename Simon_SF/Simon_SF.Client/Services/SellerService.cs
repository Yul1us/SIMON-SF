using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Shared;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

//Hace Los Llamados a la Capa del Controller... para mostrar en las Vistas de las Paginas.
namespace Blazor.SIMONStore.Client.Services
{
    public class SellerService : ISellerService
    {
       
          private readonly HttpClient _httpClient;
         public SellerService(HttpClient httpClient)
           {
             _httpClient = httpClient;
            }
     

        public async Task<Seller> GetByEmail(string Email)
        {
            return await _httpClient.GetFromJsonAsync<Seller>($"api/seller/email/{Email}");
        }

        public async Task<Seller> GetByid(int id)
        {
            return await _httpClient.GetFromJsonAsync<Seller>($"api/seller/{id}");
        }

       public async Task<IEnumerable<Seller>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Seller>>($"api/seller/");
        }

        public async Task SaveSeller(Seller seller)
        {

            await _httpClient.PutAsJsonAsync<Seller>($"api/seller/{seller.NumCodigo}", seller);
        }
    }
}
