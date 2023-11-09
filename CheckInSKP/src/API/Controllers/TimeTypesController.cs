using API.Filters;
using CheckInSKP.Application.TimeType.Queries;
using CheckInSKP.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTypesController : ControllerBase
    {
        private readonly ISender _sender;

        public TimeTypesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [AuthorizeByUserRole((int)RoleEnum.Admin, (int)RoleEnum.Staff, (int)RoleEnum.Monitor)]
        public async Task<IEnumerable<TimeTypeDto>> GetTimeTypes([FromQuery] GetTimeTypesQuery query)
        {
            return await _sender.Send(query);
        }

        [HttpGet("{timeTypeId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin, (int)RoleEnum.Staff, (int)RoleEnum.Monitor)]
        public async Task<TimeTypeDto> GetTimeTypeById([FromRoute] int timeTypeId)
        {
            var query = new GetTimeTypeByIdQuery { TimeTypeId = timeTypeId };
            return await _sender.Send(query);
        }
    }
}
