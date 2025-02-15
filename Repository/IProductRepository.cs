using Blazor.SIMONStore.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Repositories
{
    public interface IProductRepository
    {
        //Metodo para traer la Lista de los Productos, a partir del ID de la Categoria -> productCategoryId
        //Se usa para Cargar el DropDown
        Task<bool> InsertOrder(Product product);
        Task<bool> UpdateOrder(Product product);
        Task<IEnumerable<Product>> GetByCategory(int  productCategoryId);
        Task<IEnumerable<Product>> Getall();

        //Metodo para traer un unico Producto, a partir del ID del Producto -> productId
        //Se usa para Cargar el DropDown
        Task<Product> GetDetails(int productId);
        Task<Product> GetDetails(int Id, int tipo_precio, int num_tipo_cliente);

    }
}
