using System.Collections.Generic;
using System.Linq;

using JokeAPI.Data;
using JokeAPI.Interfaces;
using JokeAPI.Entities;

namespace JokeAPI.Repositories
{
    public class JokeRepository : IJokeRepository
    {
        private readonly DatabaseContextClass _context;

        public JokeRepository(DatabaseContextClass context)
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
            if (jokeToRemove != null)
            {
                _context.Jokes.Remove(jokeToRemove);
                _context.SaveChanges();
            }
        }
    }
}
