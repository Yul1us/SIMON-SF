using Blazor.SIMONStore.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Client.Services
{
    public class CostumerService : ICostumerService
    {

        //Injectamos el HttpClient
        //Usar el Servicio de Http que esta ya en Program.cs del Cliente.
        private readonly HttpClient _httpClient;
        public CostumerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        

        public async Task<IEnumerable<Costumer>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Costumer>> ($"api/costumer");
        }

        public async Task<IEnumerable<Costumer>> GetBySeller(int id)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Costumer>>($"api/costumer/seller/{id}");
        }

        public async Task<Costumer> GetDetails(int? id)
        {
            return await _httpClient.GetFromJsonAsync<Costumer>($"api/costumer/{id}");
        }
    }
}
