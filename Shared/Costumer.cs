using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Shared
{
    public class Costumer
    {
        public int Id { get; set; }
        public string? TxtCedula { get; set; }
        public string TxtApellidos { get; set; }
        public int Tipo_Precio { get; set; }
        public int Num_Vendedor { get; set; }
        public string TxtApenomb { get; set; } // Alias NOMB_VDOR
         public string Tipo_Cliente { get; set; } 
        public string Nombre_Tipo_Precio { get; set; } 
        public int Num_Tipo_Cliente { get; set; }
    }
}
