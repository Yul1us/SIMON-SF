using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Shared;
using Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;



//Hace Los Llamados a la Capa del Controller... para mostrar en las Vistas de las Paginas.
namespace Blazor.SIMONStore.Client.Services
{
    public class PaymentService : IPaymentService
    {
        //Injectamos el HttpClient
        //Usar el Servicio de Http que esta ya en Program.cs del Cliente.
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        public async Task DeletePayOrder(int id)
        {
            await _httpClient.DeleteAsync($"api/payment/{id}");
        }

        public async Task SavePayOrder(payment order)
        {
            //Aca determinamos a quien invocamos...
            if (order.Id == 0)
                //Invocar al Insert
                await _httpClient.PostAsJsonAsync<payment>($"api/payment", order);
            else
                //Invocar al Update, Pasandole el Id de la Orden a Actualizar
                await _httpClient.PutAsJsonAsync<payment>($"api/payment/{order.Id}", order);
        }

        public async Task<IEnumerable<payment>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<payment>>($"api/payment/");
        }

        public async Task<payment> GetDetails(int id)
        {
            return await _httpClient.GetFromJsonAsync<payment>($"api/payment/{id}");
        }

        public async Task<IEnumerable<payment>> GetAllBySeller(string usuario)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<payment>>($"api/payment/seller/{usuario}");
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

        public async Task<Tasa> GetTasa(DateTime? fecha)
        {
            // Construir la URL base de la API
            var url = "api/payment/Tasa";

            // Si se pasa una fecha, agregarla como parámetro de consulta a la URL
            if (fecha.HasValue)
            {
                url += $"?fecha={fecha.Value:yyyy-MM-dd}";
            }

            // Realizar la solicitud GET a la API con la URL construida
            return await _httpClient.GetFromJsonAsync<Tasa>(url);
        }

    }
}
