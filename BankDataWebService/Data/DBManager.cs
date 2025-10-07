using BankDataWebService.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace BankDataWebService.Data
{
    public class DBManager : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite(@"Data Source = Bank.db")
                .EnableSensitiveDataLogging();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Password)
                .IsUnique();
            // needs to be run:
            //dotnet ef migrations add AddUniqueIndexToEmail
            //dotnet ef database update

            List<User> users = AddUsers(modelBuilder);
            List<Account> accounts = AddAccounts(modelBuilder, users);
            AddTransactions(modelBuilder, accounts);
        }
        private static List<User> AddUsers(ModelBuilder modelBuilder)
        {
            List<User> users = new List<User>();
            User user1 = new User()
            {
                UserId = 1,
                FirstName = "Sophia",
                LastName = "Matassa",
                Password = "9348230249",
                Email = "sophia3423@gmail.com",
                PhoneNo = 0423434332
            };
            users.Add(user1);
            User user2 = new User()
            {
                UserId = 2,
                FirstName = "John",
                LastName = "Small",
                Password = "342345435",
                Email = "john4534@gmail.com",
                PhoneNo = 0458392394
            };
            users.Add(user2);
            modelBuilder.Entity<User>().HasData(users);
            return users;
        }

        private static List<Account> AddAccounts(ModelBuilder modelBuilder, List<User> users)
        {
            List<Account> accounts = new List<Account>();
            Account account1 = new Account()
            {
                AccountNo = 1,
                Pin = 3423,
                AccountType = "savings",
                UserId = 1
            };
            accounts.Add(account1);
            Account account2 = new Account()
            {
                AccountNo = 2,
                Pin = 4353,
                AccountType = "cheque",
                UserId = 2
            };
            accounts.Add(account2);
            modelBuilder.Entity<Account>().HasData(accounts);
            return accounts;
        }

        private static void AddTransactions(ModelBuilder modelBuilder, List<Account> accounts)
        {
            List<Transaction> transactions = new List<Transaction>();
            Transaction transaction1 = new Transaction()
            {
                TransactionId = 1,
                Amount = 134,
                SendingAccountNo = 1,
                TimeStamp = "2024-12-02 16:43:03"
            };
            transactions.Add(transaction1);
            Transaction transaction2 = new Transaction()
            {
                TransactionId = 2,
                Amount = 293,
                SendingAccountNo = 2,
                TimeStamp = "2025-01-03 18:52:17"
            };
            transactions.Add(transaction2);
            modelBuilder.Entity<Transaction>().HasData(transactions);
        }
    }
}
