using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using BankBusinessTier.Models;
using BankDataWebService.Models;

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

        private UserDTO GetUser(string username)
        {
            RestRequest request = new RestRequest($"/api/Users/username/{username}", Method.Get);
            RestResponse response = _client.Execute(request);
            UserDTO user = JsonConvert.DeserializeObject<UserDTO>(response.Content);
            return user;
        }

        [HttpGet("username/{username}")]
        public IActionResult GetUserWithUsername(string username)
        {
            return Ok(GetUser(username));
        }

        [HttpGet("email/{email}")]
        public IActionResult GetUserWithEmail(string email)
        {
            RestRequest request = new RestRequest($"/api/Users/email/{email}", Method.Get);
            RestResponse response = _client.Execute(request);
            UserDTO user = JsonConvert.DeserializeObject<UserDTO>(response.Content);
            return Ok(user);
        }

        [HttpGet("account/{accountno}")]
        public IActionResult GetUserWithAccountNo(int accountNo)
        {
            RestRequest request = new RestRequest($"/api/Accounts/{accountNo}", Method.Get);
            RestResponse response = _client.Execute(request);
            AccountDTO account = JsonConvert.DeserializeObject<AccountDTO>(response.Content);
            UserDTO user = GetUser(account.UserName);
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            RestRequest request = new RestRequest("/api/Users", Method.Get);
            RestResponse response = _client.Execute(request);
            IEnumerable<UserDTO> users = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(response.Content);
            return Ok(users);
        }

        [HttpPost]
        public IActionResult CreateUser(UserDTO user)
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

        [HttpPut("{username}")]
        public IActionResult UpdateUser(string username, UserDTO user)
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

        [HttpDelete("{username}")]
        public IActionResult DeleteUser(string username)
        {
            RestRequest request = new RestRequest($"/api/Users/{username}", Method.Delete);
            RestResponse response = _client.Execute(request);
            if (response.IsSuccessful)
                return NoContent();
            else
                return NotFound();
        }

        

        [HttpPut("password/{username}")]
        public IActionResult UpdateUserPassword(string username, string password)
        {
            UserDTO user = GetUser(username);
            user.Password = password;

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

        [HttpPost("login")]
        public IActionResult UserLogin(UserLoginDTO userLogin)
        {
            if (userLogin.UserName == null || userLogin.Password == null)
            {
                return BadRequest("Username or password is missing");
            }

            UserDTO user = GetUser(userLogin.UserName);

            if (user == null || user.Password != userLogin.Password)
            {
                return BadRequest("Username or password is incorrect");
            }

            return Ok(user);
        }
    }
}
