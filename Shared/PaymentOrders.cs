using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.SIMONStore.Shared
{
    public class PaymentOrders
    {
        public int Id { get; set; }
        public int PayNumber { get; set; }
        public int ClientId { get; set; }
        public DateTime PayDate { get; set; }
        public string Methodpayment { get; set; }
        public int Total { get; set; }
        public string Comment { get; set; }
        public string IdSeller { get; set; }
        public int PayNumberSIMONId { get; set; }
        public string Ref { get; set; }
    }
}
