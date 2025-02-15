using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Repositories;
using Blazor.SIMONStore.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Transactions;

namespace BlazorApp3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerRepository _seller;

        public SellerController(ISellerRepository seller)
        {
            _seller = seller;
        }
        [HttpGet]
     
        public async Task<IEnumerable<Seller>> Get()
        {
            return await _seller.Getall();
        }
        [HttpGet("{id}")]
        public async Task<Seller> GetDetails(int id)
        {
            //Debe buscar la orden en la Base de Datos.
            return await _seller.GetDetails(id);
        }

        [HttpGet("email/{email}")]
        public async Task<Seller> GetDetailsByEmail(string email)
        {
            //Debe buscar la orden en la Base de Datos.
            return await _seller.GetDetailsbyEmail(email);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Seller seller)
        {
            //Validaciones Inicio
            if (seller == null)
                return BadRequest();

            if (seller.NumCodigo == 0)
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
                await _seller.UpdateSeller(seller);



                //Si hay algún error dentro del using se realizara el Rollback
                //Si todo va bien, es como el Commit
                scope.Complete();
            }


            return NoContent(); //Todo Ok.
        }

        /*
            [HttpPost("UserRoles")]

        public async Task<IActionResult> PostUserRol([FromBody] UserRole order)
        {
           await _userRepository.InsertOrder(order);
            return NoContent();
        }

        [HttpPut("UserRoles/{UserId}")]

        public async Task<IActionResult> PutUserRol([FromBody] UserRole order)
        {
            await _userRepository.UpdateOrder(order);
            return NoContent();
        }


        */
    }
}
