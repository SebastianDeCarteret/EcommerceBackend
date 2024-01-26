using EcommerceBackend.Data;
using EcommerceBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EcommerceBackendContext _context;

        public UsersController(EcommerceBackendContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var users = await _context.User
                .Include(user => user.Basket)
                .Include(user => user.Basket.BasketItems)
                .Include(user => user.Orders)
                .ToListAsync();
            foreach (var user in users)
            {
                MakeUserPasswordEnryptedIfNotAlready(user);
            }

            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var _ = await _context.User
                .Include(user => user.Basket)
                .Include(user => user.Orders)
                .ToListAsync();
            var user = _.Find(user => user.Id == id);


            if (user == null)
            {
                return NotFound();
            }

            MakeUserPasswordEnryptedIfNotAlready(user);

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            user.Password = HashPassword(user);

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        [HttpPatch("authenticate/username/{username}/password/{password}")]
        public async Task<ActionResult<User>> AuthUser(string username, string password)
        {
            //var doesMatch = _context.User.ToList().Exists(user => user.Username == username && user.Password == password);
            var users = await _context.User.ToListAsync();
            var user = users.Find(user => user.Username == username);
            var passwordHasher = new PasswordHasher<User>();

            if (user == null)
            {
                return NotFound();
            }

            MakeUserPasswordEnryptedIfNotAlready(user);

            if (passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success)
            {
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            else
            {
                return
                BadRequest("wrong password");
            }


        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {

            user.Password = HashPassword(user);

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        private void MakeUserPasswordEnryptedIfNotAlready(User user)
        {
            if (!(user.Password.StartsWith("AQAAAAIAAYagAAAA") && user.Password.EndsWith("==")))
            {
                var passwordHasher = new PasswordHasher<User>();
                user.Password = passwordHasher.HashPassword(user, user.Password);
                _context.SaveChanges();
            }
        }

        private string HashPassword(User user)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, user.Password);
        }
    }
}
