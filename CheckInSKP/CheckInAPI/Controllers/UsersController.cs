using CheckInSKP.Application.User.Commands.CreateUser;
using CheckInSKP.Application.User.Commands.LoginUser;
using CheckInSKP.Application.User.Queries;
using CheckInSKP.Application.User.Queries.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CheckInAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser(ISender sender, [FromBody] CreateUserCommand command)
        {
            await sender.Send(command);
            return Ok();
        }

        [HttpGet("id")]
        [Authorize(Roles = "1,6")] // Admin = 1 and Monitor = 6
        public async Task<UserDto> GetUserById(ISender sender, [FromQuery] GetUserByIdQuery query)
        {
            return await sender.Send(query);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(ISender sender, [FromBody] LoginUserCommand command)
        {
            var result = await sender.Send(command);
            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
