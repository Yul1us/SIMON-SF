using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Shared;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<payment>> GetAll();
        Task<payment> GetDetails( int id);
        Task<IEnumerable<payment>> Getbyclient( int id);
        Task<IEnumerable<payment>> GetbySeller(string usuario);
        Task<bool> InsertOrder(payment order);
        Task<bool> UpdateOrder(payment order);
        Task<int> GetNextNumber();
        Task<Tasa> GetTasa(DateTime? fecha);
        //Para obtener el Prximo Id de Orden
        Task<int> GetNextId();

        Task DeletePayment(int id);
    }
}
