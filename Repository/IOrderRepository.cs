using Blazor.SIMONStore.Shared;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> InsertOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task DeleteOrder(int id);
        //Para obtener el Prximo Numero de Orden
        Task<int> GetNextNumber();

        //Para obtener el Prximo Id de Orden
        Task<int> GetNextId();

        //Para ser Mostrado en la Vista OrdersList.razor, Para tener todos los elementos u Ordenes realizadas.
        Task<IEnumerable<Order>> GetAll();

        Task<Tasa> GetTasa(DateTime? fecha);
        //Para ser mostrado en la Vista OrdersDetails.razor, Para obtener una sola Orden con enviando su id.
        Task<Order> GetDetails(int id);
        Task<IEnumerable<Order>> GetAllByIdClient(int id);
        Task<IEnumerable<Order>> GetAllByIdSeller(int id);
    }
}
