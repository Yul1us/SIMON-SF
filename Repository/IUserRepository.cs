using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository
    {

        Task<IEnumerable<User>> Getall();   
        Task<IEnumerable<Roles>> GetallRoles();
        Task<IEnumerable<UserRole>> GetallUserRoles();
        Task<bool> InsertOrder(UserRole userRole);
        Task<bool> UpdateOrder(UserRole userRole);

        Task<IEnumerable<Roles>> GetUserRoles(string userId);
        //Metodo para traer un unico Producto, a partir del ID del Producto -> productId
        //Se usa para Cargar el DropDown
        Task<User> GetDetails(int userid);
    }
}
