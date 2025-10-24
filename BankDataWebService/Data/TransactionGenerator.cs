using BankDataWebService.Models;

namespace BankDataWebService.Data
{
    public class TransactionGenerator
    {
        private static Random random = new Random();

        public static Transaction GetTransaction(int accountNo)
        {
            Transaction transaction = new Transaction
            {
                Description = GetDescription(),
                Amount = GetAmount(),
                AccountNo = accountNo
            };
            return transaction;
        }

        public static string GetDescription()
        {
            List<string> descriptions = new List<string>
            {
                "Purchase of groceries at SuperMart", "ATM withdrawal from Main Street branch", "Payment for online subscription",
                "Deposit from paycheck", "Transfer to savings account", "Purchase of electronics from TechZone", "Payment for utility bill",
                "Transfer from savings account", "Cash withdrawal at Downtown ATM", "Purchase of coffee at Brew Coffee Shop",
                "Deposit for rent payment", "Payment for mobile phone bill", "Subscription renewal for streaming service",
                "Refund for returned item at FashionHub", "Online shopping at BestBuy", "Gift deposit from family",
                "Payment for medical bill at City Hospital", "Transfer to friend via mobile wallet", "Purchase of a concert ticket",
                "Payment for online course enrollment", "Deposit from freelance work", "Transfer to investment account",
                "Payment for monthly insurance premium", "Purchase of books from Bookstore", "Transaction fee for international transfer",
                "Refund for cancelled flight", "Grocery shopping at GreenGrocer", "Purchase of gym membership"
            };
            return descriptions[random.Next(descriptions.Count)];
        }

        public static decimal GetAmount()
        {
            return random.Next(1, 100);
        }
    }
}
