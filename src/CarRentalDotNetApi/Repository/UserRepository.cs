using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly UserManager<AppUser> _userManager;
        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Email.Equals(email.ToLower()));
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(u => u.Email.Equals(email));
        }
    }
}
