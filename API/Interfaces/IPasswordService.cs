namespace JokeAPI.Interfaces
{
    public interface IPasswordService
    {
        string GenerateSalt();
        string HashPassword(string password, string salt);
        bool VerifyPassword(string hash, string password, string salt);
    }
}