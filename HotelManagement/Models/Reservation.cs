using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }

        // Strane ključevi (foreign keys)
        public int GuestID { get; set; }
        public int RoomID { get; set; }
        public int UserID { get; set; }
        
    }
}
