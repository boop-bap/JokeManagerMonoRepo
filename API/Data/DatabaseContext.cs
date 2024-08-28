using Microsoft.EntityFrameworkCore;
using JokeAPI.Entities;

namespace JokeAPI.Data.DatabaseContext
{
    public class DatabaseContextClass : DbContext
    {
        public DatabaseContextClass(DbContextOptions<DatabaseContextClass> options) : base(options) { }
        public DbSet<Joke> Jokes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}