using System;
using System.Collections.Generic;

using JokeAPI.Entities;
using JokeAPI.Interfaces;

using Shared.DTO;


namespace JokeAPI.Services
{
    public class JokeService : IJokeService
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

        public Joke AddJoke(JokeDTO joke)
        {
            Joke newJoke = new Joke { Content = joke.Content, Category = joke.Category, UserId = joke.UserId };
            _jokeRepository.AddJoke(newJoke);

            return newJoke;
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
