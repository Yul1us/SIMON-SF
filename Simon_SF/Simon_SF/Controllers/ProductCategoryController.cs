using Blazor.SIMONStore.Repositories;
using Blazor.SIMONStore.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Server.Controllers
{
    //Agrega la Seguridad necesaria para que no entren sin autorizacion... -> [Authorize]
 
    //Con ese nombre: api/[controller] -> se coloca en el navegador -> api/ProductCategory -> Para entrar aca..
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryController(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        //Decorador
        [HttpGet]
        public async Task<IEnumerable<ProductCategory>> Get()
        {
            return await _productCategoryRepository.GetAll();
        }
        
    }
}
