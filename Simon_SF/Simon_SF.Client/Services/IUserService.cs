using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Client.Services
{
    public interface IUserService
    {
        //Este Servicio debe indagar si invocar a un Insert o a un Update
        Task SaveRole(UserRole userRole);

   

        //Para retornar todas las Ordenes a la Clase Concreta [OrderService] de esta Misma Capa...
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<Roles>> GetAllRoles();
        Task<IEnumerable<Roles>> GetUserRole(string id);


     
    }
}
