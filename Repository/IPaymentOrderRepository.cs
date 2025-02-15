using Blazor.SIMONStore.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Repositories
{
    public interface IPaymentOrderRepository
    {
        Task<bool> InsertOrder(PaymentOrders order);
        Task<bool> UpdateOrder(PaymentOrders order);
        Task DeletePaymentOrder(int id);
        //Para obtener el Prximo Numero de Orden
        Task<int> GetNextNumber();

        //Para obtener el Prximo Id de Orden
        Task<int> GetNextId();

        //Para ser Mostrado en la Vista OrdersList.razor, Para tener todos los elementos u Ordenes realizadas.
        Task<IEnumerable<PaymentOrders>> GetAll();

        //Para ser mostrado en la Vista OrdersDetails.razor, Para obtener una sola Orden con enviando su id.
        Task<PaymentOrders> GetDetails(int id);
    }
}
