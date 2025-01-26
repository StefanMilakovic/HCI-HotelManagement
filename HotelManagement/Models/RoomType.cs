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
