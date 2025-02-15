using Blazor.SIMONStore.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Client.Services
{
    public class ClientService : IClientService
    {

        //Injectamos el HttpClient
        //Usar el Servicio de Http que esta ya en Program.cs del Cliente.
        private readonly HttpClient _httpClient;
        public ClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<SIMONStore.Shared.Client>> GetAll()
        {
            //Llamado en el Browser -> "api/productcategory"
            //Desde aca llamar al Controller que est en el Server
            //Tambien vamos a usar json de .Net -> GetFromJsonAsync
            //Esto lo lleva al Ultimo eslabon de la cadena que es la Vista Blazor o Blazor Component -> Pagina Blazor
            return await _httpClient.GetFromJsonAsync<IEnumerable<SIMONStore.Shared.Client>>
                ($"api/client");

            //Services debe Agregar al Archivo de dependencias Program
            //Se debe agregar el servicio de OrderService
            //builder.Services.AddScoped<IClientService, ClientService>();
        }
    }
}
