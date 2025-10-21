using System.ComponentModel.DataAnnotations;

namespace BankBusinessTier.Models
{
    public class UserDTO
    {
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? PhoneNo { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? StreetAddress { get; set; }
        public string? Suburb { get; set; }
        public string? State { get; set; }
        public int? PostalCode { get; set; }
        public string? Country { get; set; }
    }
}
