using Blazor.SIMONStore.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Client.Services
{
    public interface IPaymentOrderService
    {
        //Este Servicio debe indagar si invocar a un Insert o a un Update
        Task SavePayOrder(PaymentOrders order);

        //Para que se reconosca en la pagina...cuando se injecte el servicio
        Task<int> GetNextNumber();  

        //Para retornar todas las Ordenes a la Clase Concreta [OrderService] de esta Misma Capa...
        Task<IEnumerable<PaymentOrders>> GetAll();

        Task<PaymentOrders> GetDetails(int id);
        Task DeletePayOrder(int id);
 
    }
}
