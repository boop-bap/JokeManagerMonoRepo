namespace JokeAPI.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(int userId);
    }
}