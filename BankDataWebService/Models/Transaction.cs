using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataWebService.Models
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        public string? Description { get; set; }


        [Required]
        public int AccountNo { get; set; }
        [ForeignKey(nameof(AccountNo))]
        public virtual Account? Account { get; set; }

        public int? TargetAccountNo { get; set; } // null for withdrawals and deposits
        [ForeignKey(nameof(TargetAccountNo))]
        public virtual Account? TargetAccount { get; set; }
    }
}
 