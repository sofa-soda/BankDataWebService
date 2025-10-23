using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using BankBusinessTier.Models;
using BankDataWebService.Models;

namespace BankBusinessTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        RestClient _client;
        public AccountsController(RestClient client)
        {
            _client = client;
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            RestRequest request = new RestRequest("/api/Accounts", Method.Get);
            RestResponse response = _client.Execute(request);
            IEnumerable<AccountDTO> accounts = JsonConvert.DeserializeObject<IEnumerable<AccountDTO>>(response.Content);
            return Ok(accounts);
        }

        [HttpGet("username/{username}")]
        public IActionResult GetAccountsForUser(string username)
        {
            Console.WriteLine(username);
            RestRequest request = new RestRequest("/api/Accounts", Method.Get);
            RestResponse response = _client.Execute(request);
            var accounts = JsonConvert.DeserializeObject<IEnumerable<AccountDTO>>(response.Content);
            var filteredAccounts = accounts.Where(account => account.UserName == username).ToList();
            if (filteredAccounts.Count > 0)
            {
                return Ok(filteredAccounts);
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetAccount(int id)
        {
            RestRequest request = new RestRequest($"/api/Accounts/{id}", Method.Get);
            RestResponse response = _client.Execute(request);
            AccountDTO account = JsonConvert.DeserializeObject<AccountDTO>(response.Content);
            return Ok(account);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, AccountDTO account)
        {
            RestRequest request = new RestRequest($"/api/Accounts/{id}", Method.Put)
            {
                RequestFormat = RestSharp.DataFormat.Json,
            };
            request.AddJsonBody(account);
            RestResponse response = _client.Execute(request);
            if (response.IsSuccessful)
                return NoContent();
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult CreateAccount(AccountDTO account)
        {
            RestRequest request = new RestRequest($"/api/Accounts", Method.Post)
            {
                RequestFormat = RestSharp.DataFormat.Json,
            };
            request.AddJsonBody(account);
            RestResponse response = _client.Execute(request);
            AccountDTO newAccount = JsonConvert.DeserializeObject<AccountDTO>(response.Content);
            return Ok(newAccount);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            RestRequest request = new RestRequest($"/api/Accounts/{id}", Method.Delete);
            RestResponse response = _client.Execute(request);
            if (response.IsSuccessful)
                return NoContent();
            else
                return NotFound();
        }
    }
}
