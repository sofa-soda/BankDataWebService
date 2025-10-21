using BankDataWebService.Data;
using BankDataWebService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankDataWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly DBManager _context;

        public UsersController(DBManager context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            /*List<UserDTO> usersDTO = new List<UserDTO>(users.Count());
            for(int i = 0; i < users.Count(); i++)
            {
                usersDTO.Add(new UserDTO
                {
                    UserName = users[i].UserName,
                });
            }*/
            IEnumerable<User> users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // GET: api/Users/email/5
        [HttpGet("email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/email/5
        [HttpGet("username/{username}")]
        public async Task<ActionResult<User>> GetUserByUserName(string username)
        {
            User? user = await _context.Users
                       .FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{username}")]
        public async Task<IActionResult> PutUser(string username, User user)
        {
            if (username != user.UserName)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
                if (!UserExists(username))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserByUserName", new { username = user.UserName }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string username)
        {
            return _context.Users.Any(e => e.UserName == username);
        }
    }
}
