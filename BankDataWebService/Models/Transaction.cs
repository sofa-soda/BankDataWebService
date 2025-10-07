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
        [Required]
        public uint AccountNo { get; set; }
        public uint? TargetAccountNo { get; set; } // null for withdrawals and deposits
        [Required]
        public string TimeStamp { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public bool IsLegal { get; set; } = true;

        [Required, ForeignKey("AccountNo")]
        public Account Account { get; set; } = null!;

        [ForeignKey("TargetAccountNo")]
        public Account? TargetAccount { get; set; }
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
