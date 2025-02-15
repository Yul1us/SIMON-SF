using Blazor.SIMONStore.Repositories;
using Blazor.SIMONStore.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BlazorApp3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]

        [HttpGet("GetByCategory/{productCategoryId}")]
        public async Task<IEnumerable<Product>> GetByCategory(int productCategoryId)
        {
            return await _productRepository.GetByCategory(productCategoryId);
        }

        [HttpGet("Getall")]
        public async Task<IEnumerable<Product>> Getall()
        {
            return await _productRepository.Getall();
        }


        //Llamado en el Browser -> "api/product/{Id}"
        //Decorador Especial, ya que es necesario cambiar la Ruta, porque se debe obtener una
        //Solo Producto según el  Id, por medio de un Parametro. -> Id

        //Aca los Nombres los definimos nosotros -> GetDetails

       

        [HttpGet("{Id}")]
        public async Task<Product> GetDetails(int Id)
        {
            return await _productRepository.GetDetails(Id);
        }

        [HttpGet("{Id}/{Tipo_Precio}/{num_tipo_cliente}")]
        public async Task<Product> GetDetails(int Id, int Tipo_Precio,int num_tipo_cliente)
        {
            return await _productRepository.GetDetails(Id, Tipo_Precio, num_tipo_cliente);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            //Validaciones Inicio
            if (product == null)
                return BadRequest();

            if (product.Id == 0)
                ModelState.AddModelError("OrderNumber", "Order Number can't be empty");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Validaciones Fin


            //Usando Una Transaccion para la Orden, se debe habilitar TransactionScopeAsyncFlowOption.Enabled ya que todos nuestros metodos son Asincronos...
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //No se Requiere un Numero, ya que se usara el Existente
                //Calcular el Proximo Id de la Orden para usarlo, en order.Id
                //order.Id = await _orderRepository.GetNextId();

                //Si pasa todas las validaciones Insertamos la Orden y retornamos un 200
                await _productRepository.UpdateOrder(product);

                //Se Eliminaran todos los Detalles o Productos que ya existian en la BD, solo para crearlos nuevamentre


                //Si hay algún error dentro del using se realizara el Rollback
                //Si todo va bien, es como el Commit
                scope.Complete();
            }


            return NoContent(); //Todo Ok.
        }



    }
}
