using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamProjectServer.Models;

namespace TeamProjectServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserDataController(AppDbContext context) => _context = context;

        // GET: api/UserData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserData>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/UserData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> GetUserData(int id)
        {
            var userData = await _context.Users.FindAsync(id);

            if (userData == null)
            {
                return NotFound();
            }

            return userData;
        }

        // PUT: api/UserData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserData(int id, UserData userData)
        {
            if (id != userData.ID)
            {
                return BadRequest();
            }

            _context.Entry(userData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDataExists(id))
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

        // POST: api/UserData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserData>> PostUserData(UserData userData)
        {
            _context.Users.Add(userData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserData", new { id = userData.ID }, userData);
        }

        // DELETE: api/UserData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserData(int id)
        {
            var userData = await _context.Users.FindAsync(id);
            if (userData == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDataExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
