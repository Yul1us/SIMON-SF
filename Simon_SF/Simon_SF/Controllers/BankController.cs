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
    public class BankController : ControllerBase
    {
        private readonly IBankRepository _bank;

        public BankController(IBankRepository seller)
        {
            _bank = seller;
        }
        [HttpGet]
     
        public async Task<IEnumerable<Bank>> Get()
        {
            return await _bank.GetAll();
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
