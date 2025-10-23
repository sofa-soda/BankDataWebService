using BankBusinessTier.Models;
using BankDataWebService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace BankBusinessTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        RestClient _client;
        public TransactionsController(RestClient client)
        {
            _client = client;
        }

        private IEnumerable<TransactionDTO> PrivateGetAllTransactions()
        {
            RestRequest request = new RestRequest("/api/Transactions", Method.Get);
            RestResponse response = _client.Execute(request);
            IEnumerable<TransactionDTO> transactions = JsonConvert.DeserializeObject<IEnumerable<TransactionDTO>>(response.Content);
            return transactions;
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            RestRequest request = new RestRequest("/api/Transactions", Method.Get);
            RestResponse response = _client.Execute(request);
            IEnumerable<TransactionDTO> transactions = JsonConvert.DeserializeObject<IEnumerable<TransactionDTO>>(response.Content);
            return Ok(transactions);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetTransaction(int id)
        {
            RestRequest request = new RestRequest($"/api/Transactions/id/{id}", Method.Get);
            RestResponse response = _client.Execute(request);
            TransactionDTO transaction = JsonConvert.DeserializeObject<TransactionDTO>(response.Content);
            return Ok(transaction);
        }

        [HttpGet("account/{accountNo}")]
        public IActionResult GetTransactionsForAccount(int accountNo)
        {
            RestRequest request = new RestRequest($"/api/Transactions/account/{accountNo}", Method.Get);
            RestResponse response = _client.Execute(request);
            var transactions = JsonConvert.DeserializeObject<IEnumerable<TransactionDTO>>(response.Content);
            return Ok(transactions);
        }

        [HttpGet("user/{username}")]
        public IActionResult GetTransactionsForUser(string username)
        {
            // get accounts
            RestRequest accountRequest = new RestRequest("/api/Accounts", Method.Get);
            RestResponse accountResponse = _client.Execute(accountRequest);
            if (!accountResponse.IsSuccessful)
                return NotFound("No accounts could be found");

            var accounts = JsonConvert.DeserializeObject<IEnumerable<AccountDTO>>(accountResponse.Content);
            if (accounts == null)
                return NotFound("No accounts could be found");

            var filteredAccounts = accounts.Where(account => account.UserName == username).ToList();
            if (filteredAccounts.Count == 0)
            {
                return NotFound("No accounts for the given username");
            }

            var allTransactions = PrivateGetAllTransactions();

            if (allTransactions == null)
            {
                return NotFound("No transactions could be found");
            }

            var transactions = new List<TransactionDTO>();

            // For the accounts relating to the user, retrieve all of the transactions
            foreach (var account in filteredAccounts)
            {
                var temp = allTransactions.Where(t => t.AccountNo == account.AccountNo);
                if (temp != null)
                {
                    foreach (var transaction in temp)
                    {
                        transactions.Add(transaction);
                    }
                }
            }
            return Ok(transactions);
        }

        [HttpPost]
        public IActionResult CreateTransaction(TransactionDTO transaction)
        {
            RestRequest request = new RestRequest($"/api/Transactions", Method.Post)
            {
                RequestFormat = RestSharp.DataFormat.Json,
            };
            request.AddJsonBody(transaction);
            RestResponse response = _client.Execute(request);
            if (!response.IsSuccessful)
                return BadRequest("Something went wrong");

            TransactionDTO newTransaction = JsonConvert.DeserializeObject<TransactionDTO>(response.Content);
            return Ok(newTransaction);
        }

        [HttpGet("filter")]
        public IActionResult GetFilteredTransactions(Filter filter)
        {
            var allTransactions = PrivateGetAllTransactions();

            if (filter.filter == "above")
            {
                var filteredTransactions = allTransactions.Where(t => t.Amount > filter.amount).ToList();
                return Ok(filteredTransactions);
            }
            else if (filter.filter == "below")
            {
                var filteredTransactions = allTransactions.Where(t => t.Amount < filter.amount).ToList();
                return Ok(filteredTransactions);
            }
            else
            {
                return BadRequest("Incorrect filter provided");
            }
        }
    }
}
