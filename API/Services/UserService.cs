using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using JokeAPI.Interfaces;
using JokeAPI.Entities;
using JokeAPI.Services;

namespace JokeAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        public UserService(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<(IdentityResult, string)> AddUserAsync(User user, string password)
        {
            var newUser = new User 
            { 
                UserName = user.DisplayName, 
                DisplayName = user.DisplayName,  
                Email = user.Email, 
                Role = user.Role, 
                PasswordHash = user.PasswordHash, 
                PasswordSalt = user.PasswordSalt, 
                Permissions = user.Permissions 
            };

            var result = await _userManager.CreateAsync(newUser, password);

            if (result.Succeeded)
            {
                var token = _tokenService.GenerateJwtToken(newUser.Id);
                return (result, token);
            }

            return (result, null);
        }
    }
}