using Blazor.SIMONStore.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Repositories
{
    public interface IProductService
    {
        //Aca Retornamos el Listado de Productos
        //El Nombre lo definimos nosotros GetByCategory
        Task<IEnumerable<Product>> GetByCategory(int productCategoryId);

        //Aca Retornamos un solo Productos
        //El Nombre lo definimos nosotros GetDetails
        Task<Product> GetDetails(int Id);

        Task<IEnumerable<Product>> GetAll();
       Task<string> SubidaImagen(MultipartFormDataContent content);
        Task SaveOrder(Product product);
    
    }
}
