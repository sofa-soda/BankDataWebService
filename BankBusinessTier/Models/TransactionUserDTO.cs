using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankBusinessTier.Models
{
    public class TransactionUserDTO
    {
        public string UserName { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public int AccountNo { get; set; }
        public int? TargetAccountNo { get; set; } // null for withdrawals and deposits
    }
}
 