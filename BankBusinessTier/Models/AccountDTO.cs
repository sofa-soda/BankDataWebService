using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataWebService.Models
{
    public class AccountDTO
    {
        public int AccountNo { get; set; }
        public decimal Balance { get; set; }
        public int Pin { get; set; }
        public string AccountType { get; set; }

        public string UserName { get; set; }
    }
}
