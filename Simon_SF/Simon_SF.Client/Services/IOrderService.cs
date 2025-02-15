using Blazor.SIMONStore.Shared;
using Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Client.Services
{
    public interface IOrderService
    {
        //Este Servicio debe indagar si invocar a un Insert o a un Update
        Task SaveOrder(Order order);

        //Para que se reconosca en la pagina...cuando se injecte el servicio
        Task<int> GetNextNumber();  

        //Para retornar todas las Ordenes a la Clase Concreta [OrderService] de esta Misma Capa...
        Task<IEnumerable<Order>> GetAll();
        Task<Tasa> GetTasa(DateTime? fecha);
        Task<Order> GetDetails(int id);
        Task DeleteOrder(int id);
         Task<IEnumerable<Order>> GetByClient(int id);
         Task<IEnumerable<Order>> GetBySeller(int id);
    }
}
