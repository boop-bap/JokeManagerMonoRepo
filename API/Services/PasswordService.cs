
using System.Security.Cryptography;
using Isopoh.Cryptography.Argon2;
using System;

using JokeAPI.Interfaces;


namespace JokeAPI.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly Argon2Config _config;

        public PasswordService()
        {
            _config = new Argon2Config
            {
                Type = Argon2Type.DataDependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = 4,
                MemoryCost = 1 << 16, // 64 MB
                Lanes = 4,
                Threads = Environment.ProcessorCount,
                HashLength = 32
            };
        }
        public string GenerateSalt()
        {
            var saltBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private void SetSalt(string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            _config.Salt = saltBytes;
        }

        public string HashPassword(string password, string salt)
        {
            SetSalt(salt);

            return Argon2.Hash(password);
        }

        public bool VerifyPassword(string hash, string password, string salt)
        {
            SetSalt(salt);

            return Argon2.Verify(hash, password);
        }
    }
}