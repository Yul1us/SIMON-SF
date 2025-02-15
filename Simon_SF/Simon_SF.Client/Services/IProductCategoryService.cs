using Blazor.SIMONStore.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Blazor.SIMONStore.Client.Services
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetProductCategories();
    }
}
