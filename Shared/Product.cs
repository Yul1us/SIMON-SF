using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Shared
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price  { get; set; }
        //public int ValorUnidadTeorico { get; set; }
        public int CategoryId { get; set; }

        public string? ImageUrl { get; set; }
        public string? DescripcionCorta { get; set; }
        public string? DescripcionProducto { get; set; }
        public int ProductSIMONId { get; set; }

        public int Quantity { get; set; } //Para la Cantidad
        public int? Status { get; set; } 
        public int? Bloqueo { get; set; }
        public int? Linea { get; set; }
        public decimal? PVP1 { get; set; }
        public decimal? PVP2 { get; set; }
        public decimal? PVP3 { get; set; }
        public string? TxtUnidadMedida { get; set; }
        public int? FacturaPor { get; set; }
        public float? promedio_peso { get; set; }


    }
}
