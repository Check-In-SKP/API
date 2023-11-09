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
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            await _sender.Send(command);
            return Ok(new { Status = "Success", Message = "User created successfully." });
        }

        [HttpGet("{userId}")]
        [SecureAuthorize]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            var (userIdClaim, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (userIdClaim != userId && userRoleClaim != (int)RoleEnum.Admin)
                return Unauthorized();

            var query = new GetUserByIdQuery { UserId = userId };
            var result = await _sender.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return Unauthorized();

            return Ok(result);
        }

        [HttpGet]
        [AuthorizeByUserRole((int)RoleEnum.Admin, (int)RoleEnum.Monitor)]
        public async Task<IEnumerable<UserDto>> GetUsers([FromQuery] GetUsersQuery query)
        {
            return await _sender.Send(query);
        }

        [HttpGet("paginate")]
        [AuthorizeByUserRole((int)RoleEnum.Admin, (int)RoleEnum.Monitor)]
        public async Task<IEnumerable<UserDto>> GetUsersPaginated([FromQuery] GetUsersWithPaginationQuery query)
        {
            return await _sender.Send(query);
        }

        [HttpPut("{userId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UpdateUserCommand command)
        {
            // Checks that the user id matches the id in the command
            if (userId != command.UserId)
                return BadRequest();

            await _sender.Send(command);
            return Ok(new { Status = "Success", Message = "User updated successfully." });
        }

        [HttpPatch("{userId}/username")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateUserUsername([FromRoute] int userId, [FromBody] UpdateUserUsernameCommand command)
        {
            // Checks that the user id matches the id in the command
            if (userId != command.UserId)
                return BadRequest();

            await _sender.Send(command);
            return Ok(new { Status = "Success", Message = "User updated successfully." });
        }

        [HttpPatch("{userId}/password")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateUserPassword([FromRoute] int userId, [FromBody] UpdateUserPasswordHashCommand command)
        {
            // Checks that the user id matches the id in the command
            if(userId != command.UserId)
                return BadRequest();

            // Checks token claims to ensure that the user is authorized
            var (userIdClaim, userRoleIdClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (userIdClaim != command.UserId || userRoleIdClaim != (int)RoleEnum.Admin)
                return Unauthorized();

            await _sender.Send(command);
            return Ok(new { Status = "Success", Message = "User updated successfully." });
        }

        [HttpPatch("{userId}/role")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> UpdateUserRole([FromRoute] int userId, [FromBody] UpdateUserRoleCommand command)
        {
            // Checks that the user id matches the id in the command
            if (userId != command.UserId)
                return BadRequest();

            await _sender.Send(command);
            return Ok(new { Status = "Success", Message = "User updated successfully." });
        }

        [HttpDelete("{userId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            var command = new DeleteUserCommand { UserId = userId };
            await _sender.Send(command);
            return Ok();
        }
    }
}
