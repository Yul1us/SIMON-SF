using Blazor.SIMONStore.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Repositories
{
    public interface ICostumerRepository
    {
        Task<IEnumerable<Costumer>> GetAll();
        Task<Costumer> GetDetail(int id);
    }
}
