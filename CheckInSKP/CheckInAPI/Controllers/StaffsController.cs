using CheckInAPI.Common.Utilities;
using CheckInAPI.Filters;
using CheckInSKP.Application.Staff.Commands.CreateStaff;
using CheckInSKP.Application.Staff.Commands.LoginStaff;
using CheckInSKP.Application.Staff.Commands.UpdateStaff;
using CheckInSKP.Application.Staff.Queries;
using CheckInSKP.Application.Staff.Queries.Dtos;
using CheckInSKP.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckInAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly ISender _sender;

        public StaffsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [SecureAuthorize]
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaffCommand command)
        {
            var (userIdClaim, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);

            // Checks if the user is authorized to create a staff
            if (userIdClaim != command.UserId && userRoleClaim != (int)RoleEnum.Admin)
                return Unauthorized();

            // Checks if the user id matches the id in the command
            await _sender.Send(command);
            return Ok();
        }

        [HttpGet("{staffId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin, (int)RoleEnum.Staff, (int)RoleEnum.Monitor)] // TODO: Fix authorization
        public async Task<StaffDto> GetStaffById([FromRoute] int staffId)
        {
            var query = new GetStaffByIdQuery { StaffId = staffId };
            return await _sender.Send(query);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginStaffCommand command)
        {
            var result = await _sender.Send(command);
            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [HttpGet]
        [AuthorizeByUserRole((int)RoleEnum.Admin, (int)RoleEnum.Monitor)]
        public async Task<IEnumerable<StaffDto>> GetStaffs([FromQuery] GetStaffsQuery query)
        {
            return await _sender.Send(query);
        }

        [HttpGet("paginate")]
        [AuthorizeByUserRole((int)RoleEnum.Admin, (int)RoleEnum.Monitor)]
        public async Task<IEnumerable<StaffDto>> GetStaffsPaginated([FromQuery] GetStaffsWithPaginationQuery query)
        {
            return await _sender.Send(query);
        }

        [HttpPut("{staffId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> UpdateStaff([FromRoute] int staffId, [FromBody] UpdateStaffCommand command)
        {
            // Checks if the staff id matches the id in the command
            if (staffId != command.StaffId)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{staffId}/MeetingTime")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> UpdateStaffMeetingTime([FromRoute] int staffId, [FromBody] UpdateStaffMeetingTimeCommand command)
        {
            // Checks if the staff id matches the id in the command
            if (staffId != command.StaffId)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{staffId}/Occupation")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateStaffOccupation([FromRoute] int staffId, [FromBody] UpdateStaffOccupationCommand command)
        {
            // Checks if the staff id matches the id in the command
            if (staffId != command.StaffId)
                return BadRequest();

            var (_, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (staffId != command.StaffId || userRoleClaim != (int)RoleEnum.Admin)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{staffId}/PhoneNotitfication")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateStaffPhoneNotification([FromRoute] int staffId, [FromBody] UpdateStaffPhoneNotificationCommand command)
        {
            // Checks if the staff id matches the id in the command
            if (staffId != command.StaffId)
                return BadRequest();

            var (_, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (staffId != command.StaffId || userRoleClaim != (int)RoleEnum.Admin)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{staffId}/PhoneNumber")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateStaffPhoneNumber([FromRoute] int staffId, [FromBody] UpdateStaffPhoneNumberCommand command)
        {
            // Checks if the staff id matches the id in the command
            if (staffId != command.StaffId)
                return BadRequest();

            var (_, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (staffId != command.StaffId || userRoleClaim != (int)RoleEnum.Admin)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }
    }
}
