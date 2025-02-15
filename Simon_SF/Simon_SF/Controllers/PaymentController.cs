using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Repositories;
using Blazor.SIMONStore.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Shared;
using System.Transactions;

namespace BlazorApp3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _payment;

        public PaymentController(IPaymentRepository seller)
        {
            _payment = seller;
        }
        [HttpGet]
     
        public async Task<IEnumerable<payment>> Get()
        {
            return await _payment.GetAll();
        }
        [HttpGet("Tasa")]
        public async Task<ActionResult<Tasa>> GetTasa([FromQuery] DateTime? fecha)
        {
            try
            {
                var tasa = await _payment.GetTasa(fecha);

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
        [HttpGet("{id}")]

        public async Task<payment> Getdetails(int id)
        {
            return await _payment.GetDetails(id);
        }

        [HttpGet("Client/{id}")]

        public async Task<IEnumerable<payment>> GetByClient(int id)
        {
            return await _payment.Getbyclient(id);
        }

        [HttpGet("seller/{usuario}")]

        public async Task<IEnumerable<payment>> GetbySeller(string usuario)
        {
            return await _payment.GetbySeller(usuario);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] payment Payorder)
        {

            //Validaciones Inicio
            if (Payorder == null)
                return BadRequest();


            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Validaciones Fin


            //Usando Una Transaccion para la Orden, se debe habilitar TransactionScopeAsyncFlowOption.Enabled ya que todos nuestros metodos son Asincronos...
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Calcular el Proximo Id de la Orden para usarlo, en order.Id
   

                //Si pasa todas las validaciones Insertamos la Orden y retornamos un 200
                await _payment.InsertOrder(Payorder);
                //Si hay algún error dentro del using se realizara el Rollback
                //Si todo va bien, es como el Commit
                scope.Complete();
            }


            return NoContent(); //Todo Ok.
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] payment Payorder)
        {
            //Validaciones Inicio
            if (Payorder == null)
                return BadRequest();

            if (Payorder.Id == 0)
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
                await _payment.UpdateOrder(Payorder);



                //Si hay algún error dentro del using se realizara el Rollback
                //Si todo va bien, es como el Commit
                scope.Complete();
            }


            return NoContent(); //Todo Ok.
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _payment.DeletePayment(id);
        }
    }
}
