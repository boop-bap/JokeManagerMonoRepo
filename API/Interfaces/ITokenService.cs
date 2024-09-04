using JokeAPI.Entities;

namespace JokeAPI.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}