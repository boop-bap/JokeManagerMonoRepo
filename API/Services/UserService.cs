using System;
using System.Collections.Generic;
using JokeAPI.Entities;
using JokeAPI.Interfaces;

namespace JokeAPI.Services
{
    public class UserService
    {
        
            private readonly IUserRepository _userRepository;

            public UserService(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public void AddUser(string username, string email)
            {
                var newUser = new User {Username = username, Email = email, PasswordHash = "ILoveBanana", PasswordSalt = "4321" };
                 _userRepository.AddUser(newUser);

            }
    }
}