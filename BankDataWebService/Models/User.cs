using System.ComponentModel.DataAnnotations;

namespace BankDataWebService.Models
{
    public class User
    {
        [Required, Key]
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? PhoneNo { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? StreetAddress { get; set; }
        public string? Suburb { get; set; }
        public string? State { get; set; }
        public int? PostalCode { get; set; }
        public string? Country { get; set; }
    }
}
