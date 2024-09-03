using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

using JokeAPI.Interfaces;
using JokeAPI.Services;
using JokeAPI.Entities;
using JokeAPI.DTO;
using Shared.DTO;

namespace JokeAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;
        public UserService(UserManager<User> userManager, ITokenService tokenService, IPasswordService passwordService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _passwordService = passwordService;
        }
        public async Task<(bool Success, string Token, string ErrorMessage)> AddUserAsync(UserDTO user)
        {
            string salt = _passwordService.GenerateSalt();
            string hashedPassword = _passwordService.HashPassword(user.Password, salt);

            var newUser = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
                Role = user.Role,
                Permissions = user.Permissions
            };

            var result = await _userManager.CreateAsync(newUser);
            if (!result.Succeeded)
            {
                return (false, null, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            var token = _tokenService.GenerateJwtToken(newUser);
            return (true, token, null);
        }

        public async Task<(bool Success, string Token, string ErrorMessage)> LoginAsync(LoginDTO loginDetails)
        {
            var user = await _userManager.FindByEmailAsync(loginDetails.Email);
            if (user == null)
            {
                return (false, null, "Invalid email or password.");
            }

            var isPasswordValid = _passwordService.VerifyPassword(user.PasswordHash, loginDetails.Password, user.PasswordSalt);
            if (!isPasswordValid)
            {
                return (false, null, "Invalid email or password.");
            }

            var token = _tokenService.GenerateJwtToken(user);
            return (true, token, null);
        }
    }
}