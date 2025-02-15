using Blazor.SIMONStore.Shared;
using Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

//Hace Los Llamados a la Capa del Controller... para mostrar en las Vistas de las Paginas.
namespace Blazor.SIMONStore.Client.Services
{
    public class OrderService : IOrderService
    {
        //Injectamos el HttpClient
        //Usar el Servicio de Http que esta ya en Program.cs del Cliente.
        private readonly HttpClient _httpClient;
        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
         /*         //Aca determinamos a quien invocamos...
            if (Payorder.Id == 0)
                //Invocar al Insert
                await _httpClient.PostAsJsonAsync<PaymentOrders>($"api/paymentorders", Payorder);
            else
                //Invocar al Update, Pasandole el Id de la Orden a Actualizar
                await _httpClient.PutAsJsonAsync<PaymentOrders>($"api/paymentorders/{Payorder.Id}", Payorder);*/
        public async Task SaveOrder(Order order)
        {
            //Aca determinamos a quien invocamos...
            if (order.Id == 0)
                //Invocar al Insert
              //  await _httpClient.PostAsJsonAsync<Order>($"api/order",order);
            await _httpClient.PostAsJsonAsync<Order>($"api/order", order);
            else
                //Invocar al Update, Pasandole el Id de la Orden a Actualizar
              //  await _httpClient.PutAsJsonAsync<Order>($"api/order/{order.Id}",order);
            await _httpClient.PutAsJsonAsync<Order>($"api/order/{order.Id}", order);
        }


        public async Task<int> GetNextNumber() 
        {
            return await _httpClient.GetFromJsonAsync<int>($"api/order/getnextnumber/");
        }

        //Llamado a la API de todas las Ordenes. -> Capa del Controller [OrderController]
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Order>>($"api/order/");
        }
        //Llamado a la API de Productos de la Orden. -> Capa del Controller [OrderController]
        //Para luego mostrarlos en la Vista [OrdersDetails.razor]
        public async Task<Order> GetDetails(int id)
        {
            return await _httpClient.GetFromJsonAsync<Order>($"api/order/{id}");
        }

        public async Task DeleteOrder(int id)
        {
            await _httpClient.DeleteAsync($"api/order/{id}");
        }
        public async Task<IEnumerable<Order>> GetByClient(int id)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Order>>($"api/order/getbyclient/{id}");
        }

        public async Task<IEnumerable<Order>> GetBySeller(int id)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Order>>($"api/order/getbyIdseller/{id}");
        }
        public async Task<Tasa> GetTasa(DateTime? fecha)
        {
            // Construir la URL base de la API
            var url = "api/Order/Tasa";

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
