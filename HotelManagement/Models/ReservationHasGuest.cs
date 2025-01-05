using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public class ReservationHasGuest
    {
        public int ReservationId {  get; set; }
        public int GuestId {  get; set; }
    }
}
