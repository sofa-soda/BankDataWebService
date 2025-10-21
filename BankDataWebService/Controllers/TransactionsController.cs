using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankDataWebService.Data;
using BankDataWebService.Models;

namespace BankDataWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly DBManager _context;

        public TransactionsController(DBManager context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        // GET: api/Transactions/4
        [HttpGet("{accountNo}")]
        public async Task<ActionResult<List<Transaction>>> GetTransactionFromAccountNo(int accountNo)
        {
            // selects all transactions that have matching accountNo and puts into a list
            List<Transaction> transactions = await _context.Transactions
                        .Where(t => t.AccountNo == accountNo)
                        .OrderByDescending(t => t.TransactionId)
                        .ToListAsync();
            if (transactions == null)
            {
                return NotFound();
            }
            return transactions;
        }
        
        // check if exception handling required to check if accounts exist -------------------------
        // POST: api/Transactions
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            try
            {
                // retrieve account
                Account? account = await _context.Accounts
                        .FirstOrDefaultAsync(a => a.AccountNo == transaction.AccountNo)
                        ?? throw new Exception("account does't exist");

                if (transaction.TargetAccountNo != null)
                {
                    if (transaction.Amount <= 0)
                        throw new Exception("Transfer amount must be positive");

                    // retrieve target account
                    Account? targetAccount = await _context.Accounts
                        .FirstOrDefaultAsync(a => a.AccountNo == transaction.TargetAccountNo)
                        ?? throw new Exception("Target account does't exist");

                    if (account.Balance < transaction.Amount)
                        throw new Exception("Insufficient funds");

                    // transfer
                    account.Balance -= transaction.Amount;
                    targetAccount.Balance += transaction.Amount;
                }
                else if (transaction.TargetAccountNo == null)
                {
                    if (transaction.Amount >= 0) // deposit
                    {
                        account.Balance += transaction.Amount;
                    }
                    else if (transaction.Amount < 0) // withdrawal
                    {
                        if (account.Balance < Math.Abs(transaction.Amount))
                            throw new Exception("Insufficient funds");

                        account.Balance += transaction.Amount;
                    }
                }
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTransaction", new { id = transaction.TransactionId }, transaction);
            }
            catch (Exception ex)
            {
                transaction.IsLegal = false;
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTransaction", new { id = transaction.TransactionId }, transaction);
            }
        }
    }
}
