using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankBusinessTier.Models
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Description { get; set; }
        public int AccountNo { get; set; }
        public int? TargetAccountNo { get; set; } // null for withdrawals and deposits
    }
}
 