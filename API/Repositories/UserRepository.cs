using System.Linq;
using JokeAPI.Entities;
using JokeAPI.Interfaces;
using JokeAPI.Data.DatabaseContext;

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