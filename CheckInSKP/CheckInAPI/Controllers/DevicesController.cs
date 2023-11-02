using CheckInSKP.Application.Device.Commands.CreateDevice;
using CheckInSKP.Application.Device.Commands.DeleteDevice;
using CheckInSKP.Application.Device.Commands.UpdateDevice;
using CheckInSKP.Application.Device.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckInAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDevice(ISender sender, [FromBody] CreateDeviceCommand command)
        {
            await sender.Send(command);
            return Ok();
        }

        [HttpGet("id")]
        public async Task<DeviceDto> GetDeviceById(ISender sender, [FromQuery] GetDeviceByIdQuery query)
        {
            return await sender.Send(query);
        }

        [HttpPut("label")]
        public async Task<IActionResult> UpdateDevice(ISender sender, [FromBody] UpdateDeviceLabelCommand command)
        {
            await sender.Send(command);
            return Ok();
        }

        [HttpPut("authorized")]
        public async Task<IActionResult> AuthorizeDevice(ISender sender, [FromBody] UpdateDeviceAuthorizationCommand command)
        {
            await sender.Send(command);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteDevice(ISender sender, [FromQuery] DeleteDeviceCommand command)
        {
            await sender.Send(command);
            return Ok();
        }

        [HttpGet("query")]
        public async Task<IEnumerable<DeviceDto>> GetDevicesWithPagination(ISender sender, [FromQuery] GetDevicesWithPaginationQuery query)
        {
            return await sender.Send(query);
        }

        [HttpGet]
        public async Task<IEnumerable<DeviceDto>> GetAllDevices(ISender sender)
        {
            return await sender.Send(new GetDevicesQuery());
        }
    }
}
