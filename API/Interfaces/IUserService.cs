using System.Threading.Tasks;

using JokeAPI.Entities;
using JokeAPI.DTO;
using Shared.DTO;

namespace JokeAPI.Interfaces
{
    public interface IUserService
    {
        Task<(bool Success, string Token, string ErrorMessage)> AddUserAsync(UserDTO user);
        Task<(bool Success, string Token, string ErrorMessage)> LoginAsync(LoginDTO loginDetails);
    }
}