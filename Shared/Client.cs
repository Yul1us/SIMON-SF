using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Shared
{
    public class Client
    {
        //Id, FirstName, LastName, BirthDate, Phone, Address, ClienteSIMONId, RIFCliente, NombreCliente, Telefono, Email
        public int Id { get; set; }
        public string FirstName { get; set; } //NOMBRE COMPLETO DEL CLIENTE
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
        public int ClienteSIMONId { get; set; }
        public string RIFCliente { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
