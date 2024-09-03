using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using JokeAPI.Interfaces;
using JokeAPI.Services;
using JokeAPI.Entities;
using JokeAPI.DTO;
using Shared.DTO;

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


        [HttpPost("register")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO user)
        {
            try
            {
                var (success, token, errorMessage) = await _userService.AddUserAsync(user);

                if (success && token != null)
                {
                    return Ok(new { Token = token });
                }

                return BadRequest(new { Error = errorMessage });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO loginDetails)
        {
            try
            {
                var (success, token, errorMessage) = await _userService.LoginAsync(loginDetails);

                if (success)
                {
                    return Ok(new { Token = token });
                }

                return Unauthorized(new { Error = errorMessage });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}