using CheckInAPI.Common.Utilities;
using CheckInAPI.Filters;
using CheckInSKP.Application.User.Commands.CreateUser;
using CheckInSKP.Application.User.Commands.DeleteUser;
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
            return CreatedAtAction(nameof(GetUserById), new { id = command.Username }, command); // TODO: Fix return from command
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
        [AuthorizeUserRole(1, 6)] // Restricts access to Admin = 1 and Monitor = 6
        public async Task<IEnumerable<UserDto>> GetUsers(ISender sender, [FromQuery] GetUsersQuery query)
        {
            return await sender.Send(query);
        }

        [HttpGet("paginate")]
        [AuthorizeUserRole(1, 6)] // Restricts access to Admin = 1 and Monitor = 6
        public async Task<IEnumerable<UserDto>> GetUsersPaginated(ISender sender, [FromQuery] GetUsersWithPaginationQuery query)
        {
            return await sender.Send(query);
        }

        [HttpPut]
        [AuthorizeUserRole(1)] // Restricts access to Admin = 1
        public async Task<IActionResult> UpdateUser(ISender sender, [FromBody] UpdateUserCommand command)
        {
            await sender.Send(command);
            return Ok(new { Status = "Success", Message = "User updated successfully." });
        }

        [HttpPut("username")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateUserUsername(ISender sender, [FromBody] UpdateUserUsernameCommand command)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);

            // Parse the claims to int
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized();
            if (userRoleClaim == null || !int.TryParse(userRoleClaim.Value, out var userRole))
                return Unauthorized();

            // Check if the user is the same as the one in the token or has admin privileges
            if (userId != command.UserId || userRole != 1)
                return Unauthorized();

            await sender.Send(command);
            return Ok(new { Status = "Success", Message = "User updated successfully." });
        }

        [HttpPut("password")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateUserPassword(ISender sender, [FromBody] UpdateUserPasswordHashCommand command)
        {
            var (userId, userRoleId) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (!userId.HasValue || !userRoleId.HasValue)
                return Unauthorized();

            if (userId != command.UserId || userRoleId != 1)
                return Unauthorized();

            await sender.Send(command);
            return Ok(new { Status = "Success", Message = "User updated successfully." });
        }

        [HttpPut("role")]
        [AuthorizeUserRole(1)] // Restricts access to Admin = 1
        public async Task<IActionResult> UpdateUserRole(ISender sender, [FromBody] UpdateUserRoleCommand command)
        {
            await sender.Send(command);
            return Ok(new { Status = "Success", Message = "User updated successfully." });
        }

        [HttpDelete("id")]
        [AuthorizeUserRole(1)] // Restricts access to Admin = 1
        public async Task<IActionResult> DeleteUser(ISender sender, [FromQuery] DeleteUserCommand command)
        {
            await sender.Send(command);
            return Ok();
        }
    }
}
