using System.ComponentModel.DataAnnotations;

namespace JokeAPI.Configurations
{
    public class JwtSettings
    {
        [Required]
        public string Issuer { get; set; }

        [Required]
        public string Audience { get; set; }

        [Required]
        [MinLength(64, ErrorMessage = "Private key must be at least 64 characters long.")]
        public string PrivateKey { get; set; }

        [Required]
        [MinLength(64, ErrorMessage = "Public key must be at least 64 characters long.")]
        public string PublicKey { get; set; }

        [Required]
        [Range(1, 1440, ErrorMessage = "Token expiry must be between 1 and 1440 minutes.")]
        public int TokenExpiryMinutes { get; set; }
    }
}