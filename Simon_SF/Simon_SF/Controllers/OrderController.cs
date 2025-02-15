using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Repositories;
using Blazor.SIMONStore.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Blazor.SIMONStore.Server.Controllers
{
    //Agrega la Seguridad necesaria para que no entren sin autorizacion... -> [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //Con 2 pasos agregamo la dependencia del Repositorio...

        //Paso#1: Injectamos el Repositorio: IOrderRepository
        //Usamos la convencion de una variable global -> _orderRepository

        //Aca se van a Injectar 2 repositorios
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductRepository _orderProductRepository;

        //Paso#2: Agregamos el contructor e igualamos la var Global con la Local...
        //Agregando 2 constructores, ya que hay 2 repositorios arriba
        public OrderController(IOrderRepository orderRepository, IOrderProductRepository orderProductRepository)
        {
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
        }


        //Agregamos un metodo de percistencia... Post -> Para crear una nueva Orden...
        //El IActionResult -> Es el Response ->  del http... Retornando (200, 400, ... para indicar lo que ocurrio con nuestra petición)
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order) 
        {
            //Validaciones Inicio
            if (order == null)
                return BadRequest();

            if (order.OrderNumber == 0)
                ModelState.AddModelError("OrderNumber", "Order Number can't be empty");

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            //Validaciones Fin


            //Usando Una Transaccion para la Orden, se debe habilitar TransactionScopeAsyncFlowOption.Enabled ya que todos nuestros metodos son Asincronos...
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Calcular el Proximo Id de la Orden para usarlo, en order.Id
                order.Id = await _orderRepository.GetNextId();

                //Si pasa todas las validaciones Insertamos la Orden y retornamos un 200
                await _orderRepository.InsertOrder(order);

                if (order.Products!= null && order.Products.Any())
                {
                    //Si hay algo y no es nulo
                    foreach (var product in order.Products)
                    {
                        //Insert OrderProduct
                        await _orderProductRepository.InsertOrderProduct(order.Id, product);    

                    }
                }
                
                //Si hay algún error dentro del using se realizara el Rollback
                //Si todo va bien, es como el Commit
                scope.Complete();
            }

              
            return NoContent(); //Todo Ok.
        }
        [HttpGet("Tasa")]
        public async Task<ActionResult<Tasa>> GetTasa([FromQuery] DateTime? fecha)
        {
            try
            {
                var tasa = await _orderRepository.GetTasa(fecha);

                if (tasa == null)
                {
                    return NotFound("No se encontró la tasa para la fecha especificada.");
                }

                return Ok(tasa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        //Agregamos un metodo de percistencia... Put -> Para Actualizar una Orden Existente = id...-> [HttpPut("{id}")]
        //El IActionResult -> Es el Response ->  del http... Retornando (200, 400, ... para indicar lo que ocurrio con nuestra petición)
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Order order)
        {
            //Validaciones Inicio
            if (order == null)
                return BadRequest();

            if (order.OrderNumber == 0)
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
                await _orderRepository.UpdateOrder(order);

                //Se Eliminaran todos los Detalles o Productos que ya existian en la BD, solo para crearlos nuevamentre
                await _orderProductRepository.DeleteOrderProductByOrder(order.Id);

                //Aca Se Crearan nuevamente todos los Productos, Con los Cambios que se realizaron en la Vista Actual...
                if (order.Products != null && order.Products.Any())
                {
                    //Si hay algo y no es nulo
                    foreach (var product in order.Products)
                    {
                        //Insert OrderProduct
                        await _orderProductRepository.InsertOrderProduct(order.Id, product);

                    }
                }

                //Si hay algún error dentro del using se realizara el Rollback
                //Si todo va bien, es como el Commit
                scope.Complete();
            }


            return NoContent(); //Todo Ok.
        }


        [HttpGet("GetNextNumber")]
        public async  Task<int> GetNextNumber()
        {
            return await _orderRepository.GetNextNumber();
        }

        //Implementar el GetAll para todas las Ordenes.
        //Los llamados al GetAll se hacer desde la Capa de Servicio [OrderService], Para Mostrarlos en la Vista...
        [HttpGet]

        public async Task<IEnumerable<Order>> Get()
        {
           //return await _orderRepository.GetAll();
            //Para que traiga todos los Productos
            var orders = await _orderRepository.GetAll();

            foreach (var item in orders)
            {
                //Debe buscar todos los Productos que componen esta Orden.
                item.Products =(List<Product>) await _orderProductRepository.GetByOrder(item.Id);
            }
            return orders;  
            
        }

        //Implementar el GetDetails para Una sola Orden, Pasando el Id.
        [HttpGet("{id}")]
        public async Task<Order> GetDetails(int id)
        {
            //Debe buscar la orden en la Base de Datos.
            var order = await _orderRepository.GetDetails(id);

            //Debe buscar todos los Productos que componen esta Orden.
            var products = await _orderProductRepository.GetByOrder(id);
            //Chequeamos que la variable [order] traiga datos [productos], de la consulta anterior 
            
            if (order != null)
                //Si la Orden no esta vacia, le agregamos la orden los productos correspondientes
                order.Products = products.ToList();
            
            return order;
        }
        [HttpGet("getbyclient/{id}")]
        public async Task<IEnumerable<Order>> GetallByIdCLient(int id)
        {
            //Debe buscar la orden en la Base de Datos.
            var order = await _orderRepository.GetAllByIdClient(id);
            foreach (var item in order)
            {
                //Debe buscar todos los Productos que componen esta Orden.
                item.Products = (List<Product>)await _orderProductRepository.GetByOrder(item.Id);
            }
            return order;



        }

        [HttpGet("getbyIdSeller/{id}")]
        public async Task<IEnumerable<Order>> GetallByIdSeller(int id)
        {
            //Debe buscar la orden en la Base de Datos.
            var order = await _orderRepository.GetAllByIdSeller(id);
            foreach (var item in order)
            {
                //Debe buscar todos los Productos que componen esta Orden.
                item.Products = (List<Product>)await _orderProductRepository.GetByOrder(item.Id);
            }
            return order;



        }


        //Para Borrar una Orden.
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _orderRepository.DeleteOrder(id);
        }

    }
}
