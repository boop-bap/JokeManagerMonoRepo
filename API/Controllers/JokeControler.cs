using Microsoft.AspNetCore.Mvc;
using JokeAPI.Services;
using JokeAPI.Entities;
using System.Collections.Generic;

namespace JokeAPI.Controllers
{
    [ApiController]
    [Route("api/jokes")]
    public class JokeController : ControllerBase
    {
        private readonly JokeService _jokeService;

        public JokeController(JokeService jokeService)
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
        public ActionResult<Joke> Create([FromBody] Joke joke)
        {
            _jokeService.AddJoke(joke);
            return CreatedAtAction(nameof(GetById), new { id = joke.Id }, joke);
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