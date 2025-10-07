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

        // check if exception handling required to check if accounts exist -------------------------
        public async Task<IActionResult> ProcessTransaction(Transaction transaction)
        {
            await using var databaseTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (transaction.Amount <= 0 &&  transaction.TargetAccountNo != null)
                {
                    // invalid transaction
                }
                // checkpoint to revert back to if the transaction is illegal
                await databaseTransaction.CreateSavepointAsync("BeforeTransaction");

                // deposit 
                if (transaction.Amount >= 0 && transaction.TargetAccountNo == null) 
                {

                }
                // withdrawal
                else if (transaction.Amount < 0 && transaction.TargetAccountNo == null) 
                {

                }
                // transfer from the account to the target account
                else if (transaction.TargetAccountNo != null)
                {
                    
                }
                
                _context.Transactions.Add(transaction);
            } 
            catch (Exception ex)
            {
                // If a failure occurred, rollback to the savepoint
                await databaseTransaction.RollbackToSavepointAsync("BeforeTransaction");

                // notify user that transaction is illegal
                // save the transaction - don't affect accounts, set IsLegal to false
            }
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(uint id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(uint id, Transaction transaction)
        {
            if (id != transaction.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactions
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.TransactionId }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(uint id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(uint id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
