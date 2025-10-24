using BankDataWebService.Models;

namespace BankDataWebService.Data
{
    public class AccountGenerator
    {
        private static Random random = new Random();

        public static Account GetAccount(string username)
        {
            Account account = new Account
            {
                UserName = username,
                Balance = GetBalance(),
                Pin = GetPin(),
                AccountType = GetAccountType()
            };
            return account;
        }

        public static decimal GetBalance()
        {
            return random.Next(0, 1000);
        }

        public static int GetPin()
        {
            return random.Next(1000, 10000);
        }

        public static string GetAccountType()
        {
            List<string> accountTypes = new List<string>
            {
                "Savings", "Checking", "Credit", "Debit"
            };
            return accountTypes[random.Next(accountTypes.Count)];
        }
    }
}
