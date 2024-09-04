using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

using JokeAPI.Interfaces;
using JokeAPI.Entities;


namespace JokeAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateUserAsync(User user)
        {
            return await _userManager.CreateAsync(user);
        }
    }
}
