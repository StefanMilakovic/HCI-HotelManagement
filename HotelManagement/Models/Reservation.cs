namespace HotelManagement.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int GuestID { get; set; }
        public int RoomID { get; set; }
        public int UserID { get; set; }

        public string GuestName
        {
            get
            {
                using (var context = new HotelManagementContext())
                {
                    var guest = context.Guests.FirstOrDefault(g => g.GuestId == GuestID);
                    return guest != null ? $"{guest.FirstName} {guest.LastName}" : "Unknown";
                }
            }
        }

        public string UserName
        {
            get
            {
                using (var context = new HotelManagementContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.UserId == UserID);
                    return user != null ? $"{user.FirstName} {user.LastName}" : "Unknown";
                }
            }
        }
    }
}
