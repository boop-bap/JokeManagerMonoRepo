using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JokeAPI.Entities;

namespace JokeAPI.Data
{
    public class DatabaseContextClass : IdentityDbContext<User>
    {
        public DatabaseContextClass(DbContextOptions<DatabaseContextClass> options) : base(options) { }

        public DbSet<Joke> Jokes { get; set; }
    }
}