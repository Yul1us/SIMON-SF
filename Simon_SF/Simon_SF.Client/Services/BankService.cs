using Blazor.SIMONStore.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Client.Services
{
    public class BankService : IBankService
    {

        //Injectamos el HttpClient
        //Usar el Servicio de Http que esta ya en Program.cs del Cliente.
        private readonly HttpClient _httpClient;
        public BankService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Bank>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Bank>> ($"api/bank");
        }
    }
}
