using System.ComponentModel.DataAnnotations;

namespace BankBusinessTier.Models
{
    public class UserLoginDTO
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; }
    }
}
