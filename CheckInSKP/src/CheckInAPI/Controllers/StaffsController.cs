using CheckInAPI.Common.Utilities;
using CheckInAPI.Filters;
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
            if (userIdClaim != command.UserId && userRoleClaim != (int)RoleEnum.Admin)
                return Unauthorized();

            await _sender.Send(command);
            return Ok();
        }

        [HttpGet("{staffId}")]
        [SecureAuthorize]
        public async Task<IActionResult> GetStaffById([FromRoute] int staffId)
        {
            var (userIdClaim, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (userIdClaim != staffId && userRoleClaim != (int)RoleEnum.Admin)
                return Unauthorized();

            var query = new GetStaffByIdQuery { StaffId = staffId };
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

        [HttpPut("{staffId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> UpdateStaff([FromRoute] int staffId, [FromBody] UpdateStaffCommand command)
        {
            if (staffId != command.StaffId)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{staffId}/MeetingTime")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> UpdateStaffMeetingTime([FromRoute] int staffId, [FromBody] UpdateStaffMeetingTimeCommand command)
        {
            if (staffId != command.StaffId)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{staffId}/Occupation")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateStaffOccupation([FromRoute] int staffId, [FromBody] UpdateStaffOccupationCommand command)
        {
            var (userIdClaim, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (staffId != command.StaffId || userIdClaim != staffId && userRoleClaim != (int)RoleEnum.Admin)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{staffId}/PhoneNotitfication")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateStaffPhoneNotification([FromRoute] int staffId, [FromBody] UpdateStaffPhoneNotificationCommand command)
        {
            var (userIdClaim, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (staffId != command.StaffId || userIdClaim != staffId && userRoleClaim != (int)RoleEnum.Admin)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{staffId}/PhoneNumber")]
        [SecureAuthorize]
        public async Task<IActionResult> UpdateStaffPhoneNumber([FromRoute] int staffId, [FromBody] UpdateStaffPhoneNumberCommand command)
        {
            var (userIdClaim, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (staffId != command.StaffId || userIdClaim != staffId && userRoleClaim != (int)RoleEnum.Admin)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpDelete("{staffId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> DeleteStaff([FromRoute] int staffId)
        {
            var command = new DeleteStaffCommand { StaffId = staffId };
            await _sender.Send(command);
            return Ok();
        }

        [HttpPost("{staffId}/TimeLog")]
        [SecureAuthorize]
        public async Task<IActionResult> CreateTimeLog([FromRoute] int staffId, [FromBody] CreateStaffTimeLogCommand command)
        {
            var (userIdClaim, userRoleClaim) = ClaimUtility.ParseUserAndRoleClaims(User);
            if (staffId != command.StaffId || userIdClaim != staffId && userRoleClaim != (int)RoleEnum.Admin)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpDelete("{staffId}/TimeLog/{timeLogId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> DeleteTimeLog([FromRoute] int staffId, [FromRoute] int timeLogId)
        {
            var command = new DeleteStaffTimeLogCommand { StaffId = staffId, TimeLogId = timeLogId };
            await _sender.Send(command);
            return Ok();
        }
    }
}
