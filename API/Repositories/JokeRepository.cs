using System.Collections.Generic;
using System.Linq;
using JokeAPI.Entities;
using JokeAPI.Interfaces;
using JokeAPI.Repositories.JokeContext;

namespace JokeAPI.Repositories
{
    public class JokeRepository : IJokeRepository
    {
        private readonly JokeContext.JokeContextClass _context;

        public JokeRepository(JokeContext.JokeContextClass context)
        {
            _context = context;
        }

        public IEnumerable<Joke> GetAll()
        {
            return _context.Jokes.ToList();
        }

        public Joke? GetById(int id)
        {
            return _context.Jokes.FirstOrDefault(j => j.Id == id);
        }


        public void AddJoke(Joke joke)
        {
            _context.Jokes.Add(joke);
            _context.SaveChanges();
        }

        public void RemoveJoke(int id)
        {
            var jokeToRemove = GetById(id);
            _context.Jokes.Remove(jokeToRemove);
            _context.SaveChanges();
        }
    }
}
