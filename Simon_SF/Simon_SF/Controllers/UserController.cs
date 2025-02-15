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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
     
        public async Task<IEnumerable<User>> Get()
        {
            return await _userRepository.Getall();
        } 
        [HttpGet("Roles")]
     
        public async Task<IEnumerable<Roles>> GetRol()
        {
            return await _userRepository.GetallRoles();
        }

        [HttpGet("UserRoles")]

        public async Task<IEnumerable<UserRole>> GetUserRol()
        {
            return await _userRepository.GetallUserRoles();
        }

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

        [HttpGet("roles/{userId}")]
        public async Task<IEnumerable<Roles>> GetUserRoles(string userId) 
        {

          var roles = await _userRepository.GetUserRoles(userId);
           return roles;
        }



    }
}
