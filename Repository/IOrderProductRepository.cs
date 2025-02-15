using Blazor.SIMONStore.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Repositories
{
    public interface IOrderProductRepository
    {
        //Con esto vamos a poder crear, el Id, la cantidad, precio del producto... en los detalles
        Task<bool> InsertOrderProduct(int orderId, Product product);

        //Metodo para regresar todos los productos que componen una Orden
        Task<IEnumerable<Product>> GetByOrder(int orderId);

        //Metodo para Eliminar todos los productos que componen una orden
        //Esta Tecnica se esta usando, cada ves que se modifica una Orden, ya que Borra Todo y lo escribe nuevamente.
        Task<bool> DeleteOrderProductByOrder(int orderId);
    }
}
