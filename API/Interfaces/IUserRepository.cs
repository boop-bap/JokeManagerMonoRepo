using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

using JokeAPI.Entities;


namespace JokeAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(User user);
    }
}
