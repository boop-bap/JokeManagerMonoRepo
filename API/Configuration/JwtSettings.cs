namespace JokeAPI.Configuration
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
    }
}