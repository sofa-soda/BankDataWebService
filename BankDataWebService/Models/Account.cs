using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataWebService.Models
{
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint AccountNo { get; set; }
        public decimal Balance { get; set; } = 0;
        [Required]
        public uint Pin { get; set; }
        [Required]
        public required string AccountType { get; set; }

        // Foreign key
        public string UserName { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
