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
        public IActionResult GetAll()
        {
            RestRequest request = new RestRequest("/api/Accounts", Method.Get);
            RestResponse response = _client.Execute(request);
            IEnumerable<AccountDTO> accounts = JsonConvert.DeserializeObject<IEnumerable<AccountDTO>>(response.Content);
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            RestRequest request = new RestRequest($"/api/Accounts/{id}", Method.Get);
            RestResponse response = _client.Execute(request);
            AccountDTO account = JsonConvert.DeserializeObject<AccountDTO>(response.Content);
            return Ok(account);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AccountDTO account)
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
        public IActionResult Post(AccountDTO account)
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
        public IActionResult Delete(int id)
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
