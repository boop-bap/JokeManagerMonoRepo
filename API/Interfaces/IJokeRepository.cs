using JokeAPI.Entities;

namespace JokeAPI.Interfaces
{
    public interface IJokeRepository
{
    IEnumerable<Joke> GetAll();
    Joke? GetById(int id);
    void AddJoke(Joke joke);
    void RemoveJoke(int id);
}

}
