using System.ComponentModel.DataAnnotations.Schema;

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
