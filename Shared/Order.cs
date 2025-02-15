using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blazor.SIMONStore.Shared
{
    public class Order: INotifyPropertyChanged
    {
        public int Id { get; set; }

        public int OrderNumber { get; set; }

        public int? ClientId { get; set; }

        public string? ClientName { get; set; }

        [Required(ErrorMessage = "La fecha de la orden es obligatoria.")]
        [CustomValidation(typeof(Order), nameof(ValidateOrderDate))]
        public DateTime OrderDate

        {

            get => _selectedDate;

            set

            {

                if (_selectedDate != value)

                {

                    _selectedDate = value;

                    OnPropertyChanged(nameof(OrderDate));

                }

            }

        }
        private DateTime _selectedDate = DateTime.Now;

        [Required(ErrorMessage = "La fecha de entrega es obligatoria.")]
        [CustomValidation(typeof(Order), nameof(ValidateDeliveryDate))]
        public DateTime DeliveryDate { get; set; }

        public decimal Total { get; set; }

        public string? Email { get; set; }

        public string? EmailCliente { get; set; }

        public string? NombreCliente { get; set; }

        public string? RIFCliente { get; set; }

        public int? SIMONIdCliente { get; set; }

        public int? Status { get; set; }

        public int? OrderNumberSIMONId { get; set; }

        public int? ProductCategoryId { get; set; }

        public int? IdVendedor { get; set; }

        public int? IdTpCliente { get; set; }

        public int? IdTpPrecio { get; set; }

        public List<Product>? Products { get; set; }
        public string? Comentario { get; set; }
        public DateTime? Fecha_Dia { get; set; }
        public int? id_moneda_cambio { get; set; }
        public decimal? Tasa_Cambio { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        // Método de validación personalizado para fechas
        public static ValidationResult? ValidateOrderDate(DateTime orderDate, ValidationContext context)
        {
            if (orderDate > DateTime.Now)
            {
                return new ValidationResult("La fecha de la orden no puede ser en el futuro.");
            }

            if (orderDate < DateTime.Now.AddYears(-100))
            {
                return new ValidationResult("La fecha de la orden no puede ser mayor a un año en el pasado.");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult? ValidateDeliveryDate(DateTime deliveryDate, ValidationContext context)
        {
            var order = context.ObjectInstance as Order;
            if (order == null)
            {
                return ValidationResult.Success;
            }

            if (deliveryDate < order.OrderDate)
            {
                return new ValidationResult("La fecha de entrega no puede ser anterior a la fecha de la orden.");
            }

            return ValidationResult.Success;
        }

        protected virtual void OnPropertyChanged(string propertyName)

        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
