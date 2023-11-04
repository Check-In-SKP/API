using CheckInAPI.Common.Utilities;
using CheckInAPI.Filters;
using CheckInSKP.Application.User.Commands.CreateUser;
using CheckInSKP.Application.User.Commands.DeleteUser;
using CheckInSKP.Application.User.Commands.LoginUser;
using CheckInSKP.Application.User.Commands.UpdateUser;
using CheckInSKP.Application.User.Queries;
using CheckInSKP.Application.User.Queries.Dtos;
using CheckInSKP.Domain.Enums;
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
            return Ok(new { Status = "Success", Message = "User created successfully." });
        }

        [HttpGet("id")]
        [AuthorizeUserRole((int)Roles.Admin, (int)Roles.Monitor)]
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
        [AuthorizeUserRole((int)Roles.Admin, (int)Roles.Monitor)]
        public async Task<IEnumerable<UserDto>> GetUsers(ISender sender, [FromQuery] GetUsersQuery query)
        {
            return await sender.Send(query);
        }

        [HttpGet("paginate")]
        [AuthorizeUserRole((int)Roles.Admin, (int)Roles.Monitor)]
        public async Task<IEnumerable<UserDto>> GetUsersPaginated(ISender sender, [FromQuery] GetUsersWithPaginationQuery query)
        {
            return await sender.Send(query);
        }

        [HttpPut]
        [AuthorizeUserRole(1)]
        public async Task<IActionResult> UpdateUser(ISender sender, [FromBody] UpdateUserCommand command)
        {
            await sender.Send(command);
            return Ok(new { Status = "Success", Message = "User updated successfully." });
        }

        [HttpPut("username")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateUserUsername(ISender sender, [FromBody] UpdateUserUsernameCommand command)
        {
            var (userId, userRoleId) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (!userId.HasValue || !userRoleId.HasValue)
                return Unauthorized();

            if (userId != command.UserId || userRoleId != (int)Roles.Admin)
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

            if (userId != command.UserId || userRoleId != (int)Roles.Admin)
                return Unauthorized();

            await sender.Send(command);
            return Ok(new { Status = "Success", Message = "User updated successfully." });
        }

        [HttpPut("role")]
        [AuthorizeUserRole((int)Roles.Admin)]
        public async Task<IActionResult> UpdateUserRole(ISender sender, [FromBody] UpdateUserRoleCommand command)
        {
            await sender.Send(command);
            return Ok(new { Status = "Success", Message = "User updated successfully." });
        }

        [HttpDelete("id")]
        [AuthorizeUserRole((int)Roles.Admin)]
        public async Task<IActionResult> DeleteUser(ISender sender, [FromQuery] DeleteUserCommand command)
        {
            await sender.Send(command);
            return Ok();
        }
    }
}
