using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Shared
{
    public class Bank
    {
        public int CodBanco { get; set; }
        public string Nombre { get; set; }
        public string TipoCuenta { get; set; }
        public int Id_Moneda { get; set; }
        public string Sg_Moneda { get; set; }
    }
}
