using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using JokeAPI.Entities;

namespace JokeAPI.Interfaces
{
   public interface IUserService
    {
        Task<(IdentityResult, string)> AddUserAsync(User user, string password);
    }
}