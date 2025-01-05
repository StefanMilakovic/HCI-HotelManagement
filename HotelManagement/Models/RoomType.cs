using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; }
        public double PricePerNight { get; set; }
        public int MaxNumOfGuests { get; set; }


        public override string ToString()
        {
            return Name;
        }

    }
}
