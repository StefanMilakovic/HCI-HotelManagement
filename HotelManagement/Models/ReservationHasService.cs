using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    [Table("reservation_has_service")]
    public class ReservationHasService
    {
        public int ReservationHasServiceId { get; set; }
        public int Quantity { get; set; }
        public int ServiceId { get; set; }
        public int ReservationId { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
