using Microsoft.EntityFrameworkCore;
using JokeAPI.Entities;

namespace JokeAPI.Repositories.JokeContext
{
    public class JokeContextClass : DbContext
    {
        public JokeContextClass(DbContextOptions<JokeContextClass> options) : base(options) { }

        public DbSet<Joke> Jokes { get; set; }
    }
}