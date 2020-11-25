using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeSocialGuiaTrilha.Core.Models;
using RedeSocialGuiaTrilha.Data.Context;

namespace RedeSocialGuiaTrilha.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly RedeSocialGuiaTrilhaDbContext _context;

        public UserProfileController(RedeSocialGuiaTrilhaDbContext context)
        {
            _context = context;
        }

        // GET: api/UserProfile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfileModel>>> GetUserProfile()
        {
            return await _context.UserProfile.ToListAsync();
        }

        // GET: api/UserProfile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileModel>> GetUserProfileModel(Guid id)
        {
            var userProfileModel = await _context.UserProfile.FindAsync(id);

            if (userProfileModel == null)
            {
                return NotFound();
            }

            return userProfileModel;
        }

        // PUT: api/UserProfile/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{id}")]
        public async Task<IActionResult> PostUserProfileModel(Guid id, UserProfileModel userProfileModel)
        {
            if (id != userProfileModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(userProfileModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileModelExists(id))
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

        // POST: api/UserProfile
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserProfileModel>> PostUserProfileModel(UserProfileModel userProfileModel)
        {
            _context.UserProfile.Add(userProfileModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserProfileModel", new { id = userProfileModel.Id }, userProfileModel);
        }

        // DELETE: api/UserProfile/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserProfileModel>> DeleteUserProfileModel(Guid id)
        {
            var userProfileModel = await _context.UserProfile.FindAsync(id);
            if (userProfileModel == null)
            {
                return NotFound();
            }

            _context.UserProfile.Remove(userProfileModel);
            await _context.SaveChangesAsync();

            return userProfileModel;
        }

        private bool UserProfileModelExists(Guid id)
        {
            return _context.UserProfile.Any(e => e.Id == id);
        }
    }
}
