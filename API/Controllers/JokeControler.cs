using Microsoft.AspNetCore.Mvc;
using JokeAPI.Services;
using JokeAPI.Entities;

namespace JokeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JokeController : ControllerBase
    {
        private readonly JokeService _jokeService;

        public JokeController(JokeService jokeService)
        {
            _jokeService = jokeService;
        }

        [HttpGet("/all")]
        public ActionResult<IEnumerable<Joke>> Get()
        {
            var jokes = _jokeService.GetJokes();
            return Ok(jokes);
        }


        [HttpGet("/{id}")]
        public ActionResult<Joke> Get(int id)
        {
            var joke = _jokeService.GetJokeById(id);
            if (joke == null)
            {
                return NotFound();
            }
            return Ok(joke);
        }

        [HttpGet("/testas")]
        public ActionResult<Joke> Testing(int id)
        {
            
            return Ok("Hello");
        }

        [HttpPost]
        public ActionResult<Joke> Post([FromBody] Joke joke)
        {
            _jokeService.AddJoke(joke.Content, joke.Category);
            return CreatedAtAction(nameof(Get), new { id = joke.Id }, joke);
        }

        [HttpGet("/random")]
        public ActionResult<Joke> Random()
        {
            var joke = _jokeService.GetRandomJoke();
            return Ok(joke);
        }

        [HttpDelete("/{id}")]
        public ActionResult<Joke> Delete(int id)
        {
            var joke = _jokeService.DeleteJoke(id);
            if (joke == null)
            {
                return NotFound();
            }
            return Ok(joke);
        }
    }
}
