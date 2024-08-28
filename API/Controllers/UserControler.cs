using Microsoft.AspNetCore.Mvc;
using JokeAPI.Services;
using JokeAPI.Entities;

namespace JokeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/users")]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            _userService.AddUser(user.Username, user.Email);
            return Ok(user);
        }

    }
}