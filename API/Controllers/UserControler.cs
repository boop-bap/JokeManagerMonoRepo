using Microsoft.AspNetCore.Mvc;
using JokeAPI.Services;
using JokeAPI.Entities;
using JokeAPI.Interfaces;
using System.Threading.Tasks;

namespace JokeAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // // GET: api/users
        // [HttpGet]
        // public ActionResult<IEnumerable<User>> GetAll()
        // {
        //     var users = _userService.GetUsers();
        //     return Ok(users);
        // }

        // // GET: api/users/{id}
        // [HttpGet("{id}")]
        // public ActionResult<User> GetById(int id)
        // {
        //     var user = _userService.GetUserById(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(user);
        // }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user, string password)
        {
            var (result, token) = await _userService.AddUserAsync(user, password);

            if (result.Succeeded)
            {
                return Ok(new { Token = token });
            }

            return BadRequest(result.Errors);
        }

        // // DELETE: api/users/{id}
        // [HttpDelete("{id}")]
        // public ActionResult Delete(int id)
        // {
        //     var user = _userService.GetUserById(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //     _userService.DeleteUser(id);
        //     return NoContent();
        // }
    }
}