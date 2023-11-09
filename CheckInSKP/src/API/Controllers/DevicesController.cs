using API.Filters;
using CheckInSKP.Application.Device.Commands.CreateDevice;
using CheckInSKP.Application.Device.Commands.DeleteDevice;
using CheckInSKP.Application.Device.Commands.UpdateDevice;
using CheckInSKP.Application.Device.Queries;
using CheckInSKP.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly ISender _sender;

        public DevicesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateDevice([FromBody] CreateDeviceCommand command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpGet("{deviceId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin, (int)RoleEnum.Monitor)]
        public async Task<DeviceDto> GetDeviceById([FromRoute] Guid deviceId)
        {
            var query = new GetDeviceByIdQuery { DeviceId = deviceId };
            return await _sender.Send(query);
        }

        [HttpPatch("{deviceId}/label")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> UpdateDevice([FromRoute] Guid deviceId, [FromBody] UpdateDeviceLabelCommand command)
        {
            if (deviceId != command.DeviceId)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{deviceId}/authorize")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> AuthorizeDevice([FromRoute] Guid deviceId, [FromBody] AuthorizeDeviceCommand command)
        {
            if (deviceId != command.DeviceId)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpPatch("{deviceId}/deauthorize")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> DeauthorizeDevice([FromRoute] Guid deviceId, [FromBody] DeauthorizeDeviceCommand command)
        {
            if (deviceId != command.DeviceId)
                return BadRequest();

            await _sender.Send(command);
            return Ok();
        }

        [HttpDelete("{deviceId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IActionResult> DeleteDevice([FromRoute] Guid deviceId)
        {
            var command = new DeleteDeviceCommand { DeviceId = deviceId };
            await _sender.Send(command);
            return Ok();
        }

        [HttpGet("paginate")]
        [AuthorizeByUserRole((int)RoleEnum.Admin, (int)RoleEnum.Monitor)]
        public async Task<IEnumerable<DeviceDto>> GetDevicesWithPagination([FromQuery] GetDevicesWithPaginationQuery query)
        {
            return await _sender.Send(query);
        }

        [HttpGet]
        [AuthorizeByUserRole((int)RoleEnum.Admin, (int)RoleEnum.Monitor)]
        public async Task<IEnumerable<DeviceDto>> GetAllDevices()
        {
            return await _sender.Send(new GetDevicesQuery());
        }
    }
}
