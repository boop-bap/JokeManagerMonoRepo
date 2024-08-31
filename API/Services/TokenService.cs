using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using JokeAPI.Interfaces;

namespace JokeAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GenerateJwtToken(int userId)
        {
            // Retrieve the private key from configuration
            string privateKeyPem = _configuration["Jwt:PrivateKey"];
            if (string.IsNullOrEmpty(privateKeyPem))
            {
                throw new ArgumentNullException(nameof(privateKeyPem), "JWT private key cannot be null or empty.");
            }

            // Convert the PEM format key to an ECDsa object
            var ecdsa = ECDsa.Create();
            ecdsa.ImportFromPem(privateKeyPem.ToCharArray());

            // Create the security key
            var key = new ECDsaSecurityKey(ecdsa);
            var creds = new SigningCredentials(key, SecurityAlgorithms.EcdsaSha256);

            // Define the claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Create the token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            // Return the token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}