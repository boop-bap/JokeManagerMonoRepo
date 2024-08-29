using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using JokeAPI.Entities;
using JokeAPI.DTO;

namespace JokeAPI.Interfaces
{
   public interface IUserService
    {
        Task<(IdentityResult, string)> AddUserAsync(UserDto user);
    }
}