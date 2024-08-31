using System.Collections.Generic;
using JokeAPI.Entities;

namespace JokeAPI.Interfaces
{
    public interface IJokeService
    {
        IEnumerable<Joke> GetAllJokes();
        Joke GetJokeById(int id);
        void AddJoke(Joke joke);
        Joke DeleteJoke(int id);
        Joke GetRandomJoke();
    }
}