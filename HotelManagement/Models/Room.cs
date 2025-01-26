using HotelManagement.ViewModels;

namespace HotelManagement.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public int RoomNumber { get; set; }
        public int Floor { get; set; }
        public int RoomTypeId { get; set; }

        public string RoomTypeName
        {
            get
            {
                return new RoomsViewModel().RoomTypes
                    .FirstOrDefault(rt => rt.RoomTypeId == RoomTypeId)?.Name ?? "Unknown";
            }
        }

        public override string ToString()
        {
            return $"{RoomNumber}";
        }
    }

}
