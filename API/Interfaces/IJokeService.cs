using System.Collections.Generic;
using JokeAPI.Entities;
using Shared.DTO;
namespace JokeAPI.Interfaces
{
    public interface IJokeService
    {
        IEnumerable<Joke> GetAllJokes();
        Joke GetJokeById(int id);
        Joke AddJoke(JokeDTO joke);
        Joke DeleteJoke(int id);
        Joke GetRandomJoke();
    }
}