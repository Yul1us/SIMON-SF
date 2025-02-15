using Blazor.SIMONStore.Shared;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Client.Services
{
    public interface IClientService
    {
        //Ojo se coloco todo completo -> Blazor.SIMONStore.Shared.Client
        //Ya que Client es una palabra reservada
        Task<IEnumerable<Blazor.SIMONStore.Shared.Client>> GetAll();
    }
}
