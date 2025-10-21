using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataWebService.Models
{
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountNo { get; set; }
        public decimal Balance { get; set; } = 0;
        [Required]
        public int Pin { get; set; }
        [Required]
        public string AccountType { get; set; }


        [Required, ForeignKey("User")]
        public string UserName { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
