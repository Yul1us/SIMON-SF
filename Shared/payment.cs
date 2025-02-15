using Blazor.SIMONStore.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Client.Models
{
    public class payment
    {
      
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string? Referencia { get; set; }
        public string? Descripcion { get; set; }
        public string? Cod_Beneficiario { get; set; }
        public string? Beneficiario { get; set; }
        public int Cod_Banco { get; set; }
        public string? Banco { get; set; }
        public string? Sg_Moneda { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        public int Status { get; set; }
        public string? Usuario { get; set; }
        [Required(ErrorMessage = "La fecha de la orden es obligatoria.")]
        
        public DateTime Fecha_Creacion

        {

            get => _selectedDate;

            set

            {

                if (_selectedDate != value)

                {

                    _selectedDate = value;

                    OnPropertyChanged(nameof(Fecha_Creacion));

                }

            }

        }
        private DateTime _selectedDate = DateTime.Now;
        public string? Signo { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Tasa { get; set; }
        public decimal Monto_Divisa { get; set; }
        public string? Comentario { get; set; }
         public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)

        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
