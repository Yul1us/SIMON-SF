using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Repositories;
using Blazor.SIMONStore.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace BlazorApp3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostumerController : ControllerBase
    {
        private readonly ICostumerRepository _costumner;

        public CostumerController(ICostumerRepository seller)
        {
            _costumner = seller;
        }
        [HttpGet]
     
        public async Task<IEnumerable<Costumer>> Get()
        {
            return await _costumner.GetAll();
        }
        [HttpGet("Seller/{id}")]

        public async Task<IEnumerable<Costumer>> GetBySeller(int id)
        {
            return await _costumner.GetBySeller(id);
        }
        [HttpGet("{id}")]
        public async Task<Costumer> GetDetails(int id)
        {
            return await _costumner.GetDetail(id);
        }
        /*    [HttpGet("{id}")]
            public async Task<Seller> GetDetails(int id)
            {
                //Debe buscar la orden en la Base de Datos.
                return await _seller.GetDetails(id);
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
