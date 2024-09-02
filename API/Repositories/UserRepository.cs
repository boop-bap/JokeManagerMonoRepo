using System.Linq;

using JokeAPI.Data.DatabaseContext;
using JokeAPI.Interfaces;
using JokeAPI.Entities;

namespace JokeAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContextClass _context;

        public UserRepository(DatabaseContextClass context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}