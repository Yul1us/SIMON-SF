using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Shared;
using Shared;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Client.Services
{
    public interface IPaymentService
    {
        //Este Servicio debe indagar si invocar a un Insert o a un Update
        Task SavePayOrder(payment order);

        //Para que se reconosca en la pagina...cuando se injecte el servicio


        //Para retornar todas las Ordenes a la Clase Concreta [OrderService] de esta Misma Capa...
        Task<IEnumerable<payment>> GetAll();
        Task<IEnumerable<payment>> GetAllBySeller(string usuario);

        Task<payment> GetDetails(int id);
        Task<Tasa> GetTasa(DateTime? fecha);
        Task DeletePayOrder(int id);
        Task<string> SubidaImagen(MultipartFormDataContent content);
    }
}
