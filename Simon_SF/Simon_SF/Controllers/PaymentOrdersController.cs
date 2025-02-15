using Blazor.SIMONStore.Repositories;

using Blazor.SIMONStore.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;


namespace Blazor.SIMONStore.Server.Controllers
{
    //Agrega la Seguridad necesaria para que no entren sin autorizacion... -> [Authorize]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentOrdersController : ControllerBase
    {
        //Con 2 pasos agregamo la dependencia del Repositorio...

        //Paso#1: Injectamos el Repositorio: IPaymentOrderRepository
        //Usamos la convencion de una variable global -> _PaymentorderRepository

        //Aca se van a Injectar 2 repositorios
        private readonly IPaymentOrderRepository _PaymentorderRepository;



        //Paso#2: Agregamos el contructor e igualamos la var Global con la Local...
        //Agregando 2 constructores, ya que hay 2 repositorios arriba
        public PaymentOrdersController(IPaymentOrderRepository PayorderRepository)
        {
            _PaymentorderRepository = PayorderRepository;

        }


        //Agregamos un metodo de percistencia... Post -> Para crear una nueva Orden...
        //El IActionResult -> Es el Response ->  del http... Retornando (200, 400, ... para indicar lo que ocurrio con nuestra petición)
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentOrders Payorder)
        {

            //Validaciones Inicio
            if (Payorder == null)
                return BadRequest();

            if (Payorder.PayNumber == 0)
                ModelState.AddModelError("PayNumber", "Order Number can't be empty");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Validaciones Fin


            //Usando Una Transaccion para la Orden, se debe habilitar TransactionScopeAsyncFlowOption.Enabled ya que todos nuestros metodos son Asincronos...
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Calcular el Proximo Id de la Orden para usarlo, en order.Id
                Payorder.Id = await _PaymentorderRepository.GetNextId();

                //Si pasa todas las validaciones Insertamos la Orden y retornamos un 200
                await _PaymentorderRepository.InsertOrder(Payorder);
                //Si hay algún error dentro del using se realizara el Rollback
                //Si todo va bien, es como el Commit
                scope.Complete();
            }


            return NoContent(); //Todo Ok.
        }


        //Agregamos un metodo de percistencia... Put -> Para Actualizar una Orden Existente = id...-> [HttpPut("{id}")]
        //El IActionResult -> Es el Response ->  del http... Retornando (200, 400, ... para indicar lo que ocurrio con nuestra petición)
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PaymentOrders Payorder)
        {
            //Validaciones Inicio
            if (Payorder == null)
                return BadRequest();

            if (Payorder.PayNumber == 0)
                ModelState.AddModelError("PayNumber", "Order Number can't be empty");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Validaciones Fin


            //Usando Una Transaccion para la Orden, se debe habilitar TransactionScopeAsyncFlowOption.Enabled ya que todos nuestros metodos son Asincronos...
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //No se Requiere un Numero, ya que se usara el Existente
                //Calcular el Proximo Id de la Orden para usarlo, en order.Id
                //order.Id = await _PaymentorderRepository.GetNextId();

                //Si pasa todas las validaciones Insertamos la Orden y retornamos un 200
                await _PaymentorderRepository.UpdateOrder(Payorder);



                //Si hay algún error dentro del using se realizara el Rollback
                //Si todo va bien, es como el Commit
                scope.Complete();
            }


            return NoContent(); //Todo Ok.
        }


        [HttpGet("GetNextNumber")]
        public async Task<int> GetNextNumber()
        {
            return await _PaymentorderRepository.GetNextNumber();
        }

        //Implementar el GetAll para todas las Ordenes.
        //Los llamados al GetAll se hacer desde la Capa de Servicio [OrderService], Para Mostrarlos en la Vista...
        [HttpGet]
        public async Task<IEnumerable<PaymentOrders>> Get()
        {
            //return await _PaymentorderRepository.GetAll();
            //Para que traiga todos los Productos
            var Payorders = await _PaymentorderRepository.GetAll();


            return Payorders;

        }

        //Implementar el GetDetails para Una sola Orden, Pasando el Id.
        [HttpGet("{id}")]
        public async Task<PaymentOrders> GetDetails(int id)
        {
            //Debe buscar la orden en la Base de Datos.
            var order = await _PaymentorderRepository.GetDetails(id);

            return order;
        }

        //Para Borrar una Orden.
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _PaymentorderRepository.DeletePaymentOrder(id);
        }





        }   
        }   
    