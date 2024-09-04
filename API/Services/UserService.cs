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
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;
        public UserService(IUserRepository userRepository, ITokenService tokenService, IPasswordService passwordService)
        {
            _userRepository = userRepository;
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

            var result = await _userRepository.CreateUserAsync(newUser);
            if (!result.Succeeded)
            {
                return (false, null, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            var token = _tokenService.GenerateJwtToken(newUser);
            return (true, token, null);
        }

        public async Task<(bool Success, string Token, string ErrorMessage, User user)> LoginAsync(LoginDTO loginDetails)
        {
            var user = await _userRepository.FindByEmailAsync(loginDetails.Email);
            if (user == null)
            {
                return (false, null, "Invalid email or password.", null);
            }

            var isPasswordValid = _passwordService.VerifyPassword(user.PasswordHash, loginDetails.Password, user.PasswordSalt);
            if (!isPasswordValid)
            {
                return (false, null, "Invalid email or password.", null);
            }

            var token = _tokenService.GenerateJwtToken(user);
            return (true, token, null, user);
        }
    }
}