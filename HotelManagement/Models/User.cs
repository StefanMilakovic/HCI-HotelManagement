using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Role? Role { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }

    public enum Role
    {
        Administrator,
        Receptionist
    }


}
