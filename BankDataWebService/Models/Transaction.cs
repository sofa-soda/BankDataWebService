using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataWebService.Models
{
    public class Transaction
    {
        [Key]
        public uint TransactionId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public uint? SendingAccountNo { get; set; } // null for deposit
        public uint? ReceivingAccountNo { get; set; } // null for withdrawal
        [Required]
        public string TimeStamp { get; set; } = null!;
        public string? Description { get; set; }

        [ForeignKey("SendingAccountNo")]
        public Account? SendingAccount { get; set; }

        [ForeignKey("ReceivingAccountNo")]
        public Account? ReceivingAccount { get; set; }
    }

    // Try out this code so that sending and receiving can't both be null
    // public class Transaction : IValidatableObject
    // variable at bottom
    //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //    {
    //        if (SendingAccountNo == null && ReceivingAccountNo == null)
    //        {
    //            yield return new ValidationResult(
    //                "At least one of SendingAccountNo or ReceivingAccountNo must be provided.",
    //                new[] { nameof(SendingAccountNo), nameof(ReceivingAccountNo) }
    //            );
    //        }
    //    }
}
