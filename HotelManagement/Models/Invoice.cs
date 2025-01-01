using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime IssuedDate { get; set; }

        public int GuestID { get; set; }
        public int ReservationID { get; set; }
    }
}
