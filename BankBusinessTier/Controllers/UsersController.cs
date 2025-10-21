using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using BankBusinessTier.Models;

namespace BankBusinessTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        RestClient _client;
        public UsersController(RestClient client)
        {
            _client = client;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            RestRequest request = new RestRequest("/api/Users", Method.Get);
            RestResponse response = _client.Execute(request);
            IEnumerable<UserDTO> users = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(response.Content);
            return Ok(users);
        }

        [HttpGet("email/{email}")]
        public IActionResult GetWithEmail(string email)
        {
            RestRequest request = new RestRequest($"/api/Users/email/{email}", Method.Get);
            RestResponse response = _client.Execute(request);
            UserDTO user = JsonConvert.DeserializeObject<UserDTO>(response.Content);
            return Ok(user);
        }

        [HttpGet("username/{username}")]
        public IActionResult GetWithUserName(string username)
        {
            RestRequest request = new RestRequest($"/api/Users/username/{username}", Method.Get);
            RestResponse response = _client.Execute(request);
            UserDTO user = JsonConvert.DeserializeObject<UserDTO>(response.Content);
            return Ok(user);
        }

        [HttpPut("{username}")]
        public IActionResult Put(string username, UserDTO user)
        {
            RestRequest request = new RestRequest($"/api/Users/{username}", Method.Put)
            {
                RequestFormat = RestSharp.DataFormat.Json,
            };
            request.AddJsonBody(user);
            RestResponse response = _client.Execute(request);
            if (response.IsSuccessful)
                return NoContent();
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Post(UserDTO user)
        {
            RestRequest request = new RestRequest($"/api/Users", Method.Post)
            {
                RequestFormat = RestSharp.DataFormat.Json,
            };
            request.AddJsonBody(user);
            RestResponse response = _client.Execute(request);
            var status = JsonConvert.DeserializeObject<UserDTO>(response.Content);
            return Ok(status);
        }

        [HttpDelete("{username}")]
        public IActionResult Delete(string username)
        {
            RestRequest request = new RestRequest($"/api/Users/{username}", Method.Delete);
            RestResponse response = _client.Execute(request);
            if (response.IsSuccessful)
                return NoContent();
            else
                return NotFound();
        }
    }
}
