using BankDataWebService.Data;
using BankDataWebService.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DBManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DBManager>();
    dbContext.Database.Migrate();
    SeedData(dbContext);
}


app.Run();



void SeedData(DBManager _context)
{
    Random random = new Random();
    if (_context.Users.Any() || _context.Accounts.Any() || _context.Transactions.Any())
    {
        return;
    }


    List<User> users = new List<User>();
    List<Account> accounts = new List<Account>();
    for (int i = 0; i < 200; i++)
    {
        User user = UserGenerator.GetUserProfile();

        int noAccounts = random.Next(1, 5);
        for (int j = 0; j < noAccounts; j++)
        {
            Account account = AccountGenerator.GetAccount(user.UserName);
            accounts.Add(account);
        }
        users.Add(user);
    }

    _context.Users.AddRange(users);
    _context.Accounts.AddRange(accounts);
    _context.SaveChanges();


    List<Transaction> transactions = new List<Transaction>();

    foreach (var account in accounts)
    {
        int noTransactions = random.Next(20, 50);
        for (int i = 0; i < noTransactions; i++)
        {
            int transactionType = random.Next(1, 4);
            if (transactionType == 1) // deposit
            {
                Transaction transaction = TransactionGenerator.GetTransaction(account.AccountNo);
                account.Balance += transaction.Amount;
                transactions.Add(transaction);
            }
            else if (transactionType == 2) // withdrawal
            {
                Transaction transaction = TransactionGenerator.GetTransaction(account.AccountNo);
                if (account.Balance > transaction.Amount)
                {
                    transaction.Amount = transaction.Amount * -1;
                    account.Balance += transaction.Amount;
                    transactions.Add(transaction);
                }
            }
            else // account transfer
            {
                Transaction transaction = TransactionGenerator.GetTransaction(account.AccountNo);

                // retrieve a valid target account
                int tAccountNo = random.Next(1, accounts.Count);
                Account? targetAccount = accounts.Find(t => t.AccountNo == tAccountNo);

                // perform transaction
                if (targetAccount != null && account.Balance > transaction.Amount)
                {
                    transaction.TargetAccountNo = targetAccount.AccountNo;
                    account.Balance -= transaction.Amount;
                    targetAccount.Balance += transaction.Amount;
                    transactions.Add(transaction);
                }
            }
        }

    }

    _context.Transactions.AddRange(transactions);
    _context.SaveChanges();
}