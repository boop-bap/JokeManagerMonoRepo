using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using JokeAPI.Interfaces;
using JokeAPI.Services;
using JokeAPI.Entities;
using Shared.DTO;

namespace JokeAPI.Controllers
{
    [ApiController]
    [Route("api/jokes")]
    public class JokeController : ControllerBase
    {
        private readonly IJokeService _jokeService;

        public JokeController(IJokeService jokeService)
        {
            _jokeService = jokeService;
        }

        // GET: api/jokes
        [HttpGet]
        public ActionResult<IEnumerable<Joke>> GetAll()
        {
            var jokes = _jokeService.GetAllJokes();
            return Ok(jokes);
        }

        // GET: api/jokes/{id}
        [HttpGet("{id}")]
        public ActionResult<Joke> GetById(int id)
        {
            var joke = _jokeService.GetJokeById(id);
            if (joke == null)
            {
                return NotFound();
            }
            return Ok(joke);
        }

        // POST: api/jokes
        [HttpPost]
        public ActionResult<Joke> Create([FromBody] JokeDTO joke)
        {

            return _jokeService.AddJoke(joke);
        }

        // GET: api/jokes/random
        [HttpGet("random")]
        public ActionResult<Joke> GetRandom()
        {
            var joke = _jokeService.GetRandomJoke();
            return Ok(joke);
        }
    }
}