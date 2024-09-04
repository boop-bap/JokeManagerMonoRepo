using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System;

using JokeAPI.Entities;
using JokeAPI.Interfaces;
using JokeAPI.Configurations;

namespace JokeAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
        }

        public string GenerateJwtToken(User user)
        {
            // Retrieve the private key from configuration
            string privateKey = _jwtSettings.PrivateKey;

            if (string.IsNullOrEmpty(privateKey))
            {
                throw new ArgumentNullException(nameof(privateKey), "JWT private key cannot be null or empty.");
            }

            // Convert the PEM format key to an ECDsa object
            var ecdsa = ECDsa.Create();
            ecdsa.ImportFromPem(privateKey.ToCharArray());

            // Create the security key
            var key = new ECDsaSecurityKey(ecdsa);
            var creds = new SigningCredentials(key, SecurityAlgorithms.EcdsaSha256);

            // Define the claims
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),
                    new Claim("email", user.Email),
                    new Claim("role", user.Role),
                    new Claim("permissions", user.Permissions)
                };

            // Create the token
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateJwtToken(string token)
        {
            // Retrieve the public key from configuration
            string publicKey = _jwtSettings.PublicKey;

            if (string.IsNullOrEmpty(publicKey))
            {
                throw new ArgumentNullException(nameof(publicKey), "JWT public key cannot be null or empty.");
            }

            // Convert the PEM format key to an ECDsa object
            using (var ecdsa = ECDsa.Create())
            {
                ecdsa.ImportFromPem(publicKey.ToCharArray());

                // Create the security key
                var key = new ECDsaSecurityKey(ecdsa);

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Audience,
                    IssuerSigningKey = key
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                return principal;
            }
        }
    }
}