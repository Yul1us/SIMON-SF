using Blazor.SIMONStore.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;



//Hace Los Llamados a la Capa del Controller... para mostrar en las Vistas de las Paginas.
namespace Blazor.SIMONStore.Client.Services
{
    public class PaymentOrderService : IPaymentOrderService
    {
        //Injectamos el HttpClient
        //Usar el Servicio de Http que esta ya en Program.cs del Cliente.
        private readonly HttpClient _httpClient;

        public PaymentOrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        public async Task DeletePayOrder(int id)
        {
            await _httpClient.DeleteAsync($"api/PaymentOrders/{id}");
        }

        public async Task<IEnumerable<PaymentOrders>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<PaymentOrders>>($"api/paymentorders/");
        }

        public async Task<PaymentOrders> GetDetails(int id)
        {
            return await _httpClient.GetFromJsonAsync<PaymentOrders>($"api/PaymentOrders/{id}");
        }

        public async Task<int> GetNextNumber()
        {
            return await _httpClient.GetFromJsonAsync<int>($"api/paymentorders/GetNextNumber/");
        }

        public async Task  SavePayOrder(PaymentOrders Payorder)
        {
        
            //Aca determinamos a quien invocamos...
            if (Payorder.Id == 0)
                //Invocar al Insert
                await _httpClient.PostAsJsonAsync<PaymentOrders>($"api/paymentorders", Payorder);
            else
                //Invocar al Update, Pasandole el Id de la Orden a Actualizar
                await _httpClient.PutAsJsonAsync<PaymentOrders>($"api/paymentorders/{Payorder.Id}", Payorder);
        }

      
        /*   public async Task SaveOrder(Order order)
  {
      //Aca determinamos a quien invocamos...
      if (order.Id == 0)
          //Invocar al Insert
          await _httpClient.PostAsJsonAsync<Order>($"api/order", order);
      else
          //Invocar al Update, Pasandole el Id de la Orden a Actualizar
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
  }*/
    }
}
