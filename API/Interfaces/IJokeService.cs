using System;
using System.Collections.Generic;
using JokeAPI.Entities;
using JokeAPI.Interfaces;

namespace JokeAPI.Services
{
    public class JokeService
    {
        private readonly IJokeRepository _jokeRepository;

        public JokeService(IJokeRepository jokeRepository)
        {
            _jokeRepository = jokeRepository;
        }

        public IEnumerable<Joke> GetAllJokes()
        {
            return _jokeRepository.GetAll();
        }

        public Joke GetJokeById(int id)
        {
            return _jokeRepository.GetById(id);
        }

        public void AddJoke(Joke joke)
        {
            var newJoke = new Joke { Content = joke.Content, Category = joke.Category, UserId = 1 };
            _jokeRepository.AddJoke(newJoke);
        }

        public Joke DeleteJoke(int id)
        {
            var jokeToRemove = _jokeRepository.GetById(id);
            _jokeRepository.RemoveJoke(id);
            return jokeToRemove;
        }

        public Joke GetRandomJoke()
        {
            var jokes = _jokeRepository.GetAll().ToList();
            var random = new Random();
            var randomIndex = random.Next(0, jokes.Count);
            return jokes[randomIndex];
        }
    }
}
