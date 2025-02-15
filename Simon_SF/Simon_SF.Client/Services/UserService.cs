using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

//Hace Los Llamados a la Capa del Controller... para mostrar en las Vistas de las Paginas.
namespace Blazor.SIMONStore.Client.Services
{
    public class UserService : IUserService
    {
        //    //Injectamos el HttpClient
        //    //Usar el Servicio de Http que esta ya en Program.cs del Cliente.
        //    private readonly HttpClient _httpClient;
        //    public OrderService(HttpClient httpClient)
        //    {
        //        _httpClient = httpClient;
        //    }
        //    public async Task SaveOrder(Order order)
        //    {
        //        //Aca determinamos a quien invocamos...
        //        if (order.Id == 0)
        //            //Invocar al Insert
        //            await _httpClient.PostAsJsonAsync<Order>($"api/order", order);
        //        else
        //            //Invocar al Update, Pasandole el Id de la Orden a Actualizar
        //            await _httpClient.PutAsJsonAsync<Order>($"api/order/{order.Id}", order);
        //    }

        //    public async Task<int> GetNextNumber() 
        //    {
        //        return await _httpClient.GetFromJsonAsync<int>($"api/order/getnextnumber/");
        //    }

        //    //Llamado a la API de todas las Ordenes. -> Capa del Controller [OrderController]
        //    public async Task<IEnumerable<Order>> GetAll()
        //    {
        //        return await _httpClient.GetFromJsonAsync<IEnumerable<Order>>($"api/order/");
        //    }
        //    //Llamado a la API de Productos de la Orden. -> Capa del Controller [OrderController]
        //    //Para luego mostrarlos en la Vista [OrdersDetails.razor]
        //    public async Task<Order> GetDetails(int id)
        //    {
        //        return await _httpClient.GetFromJsonAsync<Order>($"api/order/{id}");
        //    }

        //    public async Task DeleteOrder(int id)
        //    {
        //        await _httpClient.DeleteAsync($"api/order/{id}");
        //    }

        ////////Injectamos el HttpClient
        ////////    //Usar el Servicio de Http que esta ya en Program.cs del Cliente.
          private readonly HttpClient _httpClient;
         public UserService(HttpClient httpClient)
           {
             _httpClient = httpClient;
            }
        public async Task<IEnumerable<User>> GetAll()
        {
                return await _httpClient.GetFromJsonAsync<IEnumerable<User>>($"api/User/");
        }

        public async Task<IEnumerable<Roles>> GetAllRoles()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Roles>>($"api/User/Roles");
        }

        public async Task<IEnumerable<Roles>> GetUserRole(string id)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Roles>>($"api/user/roles/{id}");
        }

        public async Task SaveRole(UserRole userRole)
        {
            var UserRoler = await _httpClient.GetFromJsonAsync<IEnumerable<UserRole>>($"api/User/UserRoles");
            // Suponiendo que 'userRoles' es una lista o una colección de objetos 'UserRole'
            bool roleExists = UserRoler.Any(ur => ur.UserId == userRole.UserId );

            if (!roleExists)
            {
              await _httpClient.PostAsJsonAsync<UserRole>($"api/User/UserRoles", userRole);
            }
            else
            {
                await _httpClient.PutAsJsonAsync<UserRole>($"api/User/UserRoles/{userRole.UserId}",userRole );

            }
           
        }
    }
}
