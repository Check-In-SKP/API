using API.Common.Utilities;
using API.Filters;
using CheckInSKP.Application.Staff.Commands.CreateStaff;
using CheckInSKP.Application.Staff.Commands.CreateStaffTimeLog;
using CheckInSKP.Application.Staff.Commands.DeleteStaff;
using CheckInSKP.Application.Staff.Commands.DeleteStaffTimeLog;
using CheckInSKP.Application.Staff.Commands.LoginStaff;
using CheckInSKP.Application.Staff.Commands.UpdateStaff;
using CheckInSKP.Application.Staff.Queries;
using CheckInSKP.Application.Staff.Queries.Dtos;
using CheckInSKP.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
            if (userIdClaim != command.UserId && userRoleClaim != (int)RoleEnum.Admin)
                return Unauthorized();

            await _sender.Send(command);
            return Ok();
        }

        [HttpGet("{userId}")]
        [SecureAuthorize]
        public async Task<IActionResult> GetStaffById([FromRoute] Guid userId)
        {
            var (userIdClaim, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (userIdClaim != userId && userRoleClaim != (int)RoleEnum.Admin)
                return Unauthorized();

            var query = new GetStaffByIdQuery { StaffId = userId };
            var result = await _sender.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
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

        [HttpPut("{userId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> UpdateStaff([FromRoute] Guid userId, [FromBody] UpdateStaffCommand command)
        {
            if (userId != command.StaffId)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{userId}/MeetingTime")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> UpdateStaffMeetingTime([FromRoute] Guid userId, [FromBody] UpdateStaffMeetingTimeCommand command)
        {
            if (userId != command.StaffId)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{userId}/Occupation")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateStaffOccupation([FromRoute] Guid userId, [FromBody] UpdateStaffOccupationCommand command)
        {
            var (userIdClaim, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (userId != command.StaffId || userIdClaim != userId && userRoleClaim != (int)RoleEnum.Admin)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{userId}/PhoneNotitfication")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateStaffPhoneNotification([FromRoute] Guid userId, [FromBody] UpdateStaffPhoneNotificationCommand command)
        {
            var (userIdClaim, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (userId != command.StaffId || userIdClaim != userId && userRoleClaim != (int)RoleEnum.Admin)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{userId}/PhoneNumber")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateStaffPhoneNumber([FromRoute] Guid userId, [FromBody] UpdateStaffPhoneNumberCommand command)
        {
            var (userIdClaim, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (userId != command.StaffId || userIdClaim != userId && userRoleClaim != (int)RoleEnum.Admin)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpDelete("{userId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> DeleteStaff([FromRoute] Guid userId)
        {
            var command = new DeleteStaffCommand { StaffId = userId };
            await _sender.Send(command);
            return Ok();
        }

        [HttpPost("{userId}/TimeLog")]
        [SecureAuthorize]
        public async Task<IActionResult> CreateTimeLog([FromRoute] Guid userId, [FromBody] CreateStaffTimeLogCommand command)
        {
            var (userIdClaim, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (userId != command.StaffId || userIdClaim != userId && userRoleClaim != (int)RoleEnum.Admin)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpDelete("{userId}/TimeLog/{timeLogId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> DeleteTimeLog([FromRoute] Guid userId, [FromRoute] int timeLogId)
        {
            var command = new DeleteStaffTimeLogCommand { StaffId = userId, TimeLogId = timeLogId };
            await _sender.Send(command);
            return Ok();
        }

        [HttpGet("today-timelogs")]
        [AuthorizeByUserRole((int)RoleEnum.Admin, (int)RoleEnum.Monitor)]
        public async Task<IEnumerable<StaffDto>> GetTodayTimeLogs([FromQuery] GetAvailableStaffsWithTodayTimeLogsQuery query)
        {
            return await _sender.Send(query);
        }
    }
}
