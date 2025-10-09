using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataWebService.Models
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint TransactionId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public uint AccountNo { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime TimeStamp { get; set; }
        public uint? TargetAccountNo { get; set; } // null for withdrawals and deposits
        public string? Description { get; set; }
        [Required]
        public bool IsLegal { get; set; } = true;

        // FOREIGN KEY STUFF
        //[ForeignKey("AccountNo")] // can be null to allow for complete history
        //public Account? Account { get; set; }
        //[ForeignKey("TargetAccountNo")]
        //public Account? TargetAccount { get; set; }
    }
}
 