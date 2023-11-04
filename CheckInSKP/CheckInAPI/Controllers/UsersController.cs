using CheckInAPI.Filters;
using CheckInSKP.Application.User.Commands.CreateUser;
using CheckInSKP.Application.User.Commands.LoginUser;
using CheckInSKP.Application.User.Commands.UpdateUser;
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
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(ISender sender, [FromBody] CreateUserCommand command)
        {
            await sender.Send(command);
            return Ok();
        }

        [HttpGet("id")]
        [AuthorizeUserRole(1, 6)] // Resricts access to Admin = 1 and Monitor = 6
        public async Task<UserDto> GetUserById(ISender sender, [FromQuery] GetUserByIdQuery query)
        {
            return await sender.Send(query);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(ISender sender, [FromBody] LoginUserCommand command)
        {
            var result = await sender.Send(command);
            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [HttpGet]
        [AuthorizeUserRole(1, 6)] // Resricts access to Admin = 1 and Monitor = 6
        public async Task<IEnumerable<UserDto>> GetUsers(ISender sender, [FromQuery] GetUsersQuery query)
        {
            return await sender.Send(query);
        }

        [HttpGet("Paginate")]
        [AuthorizeUserRole(1, 6)] // Resricts access to Admin = 1 and Monitor = 6
        public async Task<IEnumerable<UserDto>> GetUsersPaginated(ISender sender, [FromQuery] GetUsersWithPaginationQuery query)
        {
            return await sender.Send(query);
        }

        [HttpPut]
        [AuthorizeUserRole(1)] // Resricts access to Admin = 1
        public async Task<IActionResult> UpdateUser(ISender sender, [FromBody] UpdateUserCommand command)
        {
            await sender.Send(command);
            return Ok();
        }

        [HttpPut("username")]
        [Authorize]
        public async Task<IActionResult> UpdateUserUsername(ISender sender, [FromBody] UpdateUserUsernameCommand command)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userRole = int.Parse(User.FindFirst(ClaimTypes.Role).Value);

            if (userId != command.UserId || userRole != 1)
            {
                return Unauthorized();
            }
            await sender.Send(command);
            return Ok();
        }
    }
}
