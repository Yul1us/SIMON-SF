
using Blazor.SIMONStore.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Client.Services
{
    public class ProductService : IProductService
    {
        //Injectamos el HttpClient
        //Usar el Servicio de Http que esta ya en Program.cs del Cliente.
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Product>>($"api/product/Getall");
        }

        public async Task<IEnumerable<Product>> GetByCategory(int productCategoryId)
        {
            //Aca Retornamos el Listado de Productos
            return await _httpClient.GetFromJsonAsync<IEnumerable<Product>>($"api/product/GetByCategory/{productCategoryId}");

        }

        public async Task<Product> GetDetails(int Id)
        {
            //Aca Retornamos un solo Producto
            return await _httpClient.GetFromJsonAsync<Product>($"api/product/{Id}");
        }

        public async Task<Product> GetDetails(int Id, int Tipo_Precio, int num_tipo_cliente)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/product/{Id}/{Tipo_Precio}/{num_tipo_cliente}");
        }

        public async Task SaveOrder(Product product)
        {
            //Aca determinamos a quien invocamos...
            if (product.Id == 0)
                //Invocar al Insert
                await _httpClient.PostAsJsonAsync<Product>($"api/product", product);
            else
                //Invocar al Update, Pasandole el Id de la Orden a Actualizar
                await _httpClient.PutAsJsonAsync<Product>($"api/product/{product.Id}", product);
        }

        public async Task<string> SubidaImagen(MultipartFormDataContent content)
        { 
            var postResult = await _httpClient.PostAsync($"api/upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imagenPost = Path.Combine("", postContent);
                return imagenPost;
            }
        }
    }
}
