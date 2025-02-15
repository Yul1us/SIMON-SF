using Blazor.SIMONStore.Shared;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Net.Http.Json;

namespace Blazor.SIMONStore.Client.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        //Usar el Servicio de Http que esta ya en Program.cs del Cliente.
        private readonly HttpClient _httpClient;
        public ProductCategoryService(HttpClient httpClient)
        {
                _httpClient = httpClient; 
        }

        public async Task<IEnumerable<ProductCategory>> GetProductCategories()
        {
            //Llamado en el Browser -> "api/productcategory"
            //Desde aca llamar al Controller que est en el Server
            //Tambien vamos a usar json de .Net -> GetFromJsonAsync
            //Esto lo lleva al Ultimo eslabon de la cadena que es la Vista Blazor o Blazor Component -> Pagina Blazor
            return await _httpClient.GetFromJsonAsync<IEnumerable<ProductCategory>>($"api/productcategory");
        }
    }
}
