using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public int RoomNumber { get; set; }
        public int Floor { get; set; }
        public RoomType RoomType { get; set; }
    }

    public enum RoomType
    {
        SingleRoom,
        DoubleRoom,
        Suite,
        DeluxeRoom,
        FamilyRoom 
    }

}
