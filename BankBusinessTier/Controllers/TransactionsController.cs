using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using BankBusinessTier.Models;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            RestRequest request = new RestRequest("/api/Transactions", Method.Get);
            RestResponse response = _client.Execute(request);
            IEnumerable<TransactionDTO> accounts = JsonConvert.DeserializeObject<IEnumerable<TransactionDTO>>(response.Content);
            return Ok(accounts);
        }

        [HttpGet("{accountNo}")]
        public IActionResult Get(int accountNo)
        {
            RestRequest request = new RestRequest($"/api/Transactions/{accountNo}", Method.Get);
            RestResponse response = _client.Execute(request);
            TransactionDTO transaction = JsonConvert.DeserializeObject<TransactionDTO>(response.Content);
            return Ok(transaction);
        }

        [HttpPost]
        public IActionResult Post(TransactionDTO transaction)
        {
            RestRequest request = new RestRequest($"/api/Transactions", Method.Post)
            {
                RequestFormat = RestSharp.DataFormat.Json,
            };
            request.AddJsonBody(transaction);
            RestResponse response = _client.Execute(request);
            TransactionDTO newTransaction = JsonConvert.DeserializeObject<TransactionDTO>(response.Content);
            return Ok(newTransaction);
        }
    }
}
