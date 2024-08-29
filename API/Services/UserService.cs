using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using JokeAPI.Interfaces;
using JokeAPI.Entities;
using JokeAPI.DTO;
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

        public async Task<(IdentityResult, string)> AddUserAsync(UserDto user)
        {

              // Generate salt
            var salt = GenerateSalt();

            // Hash the password with the salt
            var hashedPassword = HashPassword(user.Password, salt);

            // Create the user entity
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

            if (result.Succeeded)
            {
                var token = _tokenService.GenerateJwtToken(newUser.Id);
                return (result, token);
            }

            return (result, null);
        }

        private static string GenerateSalt()
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
        

        private static string HashPassword(string password, string salt)
        {
            var sha256 = System.Security.Cryptography.SHA256.Create();
            var saltedPassword = password + salt;
            var saltedPasswordBytes = System.Text.Encoding.UTF8.GetBytes(saltedPassword);
            var hashBytes = sha256.ComputeHash(saltedPasswordBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}