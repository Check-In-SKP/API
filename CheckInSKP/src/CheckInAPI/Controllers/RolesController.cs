using CheckInAPI.Filters;
using CheckInSKP.Application.Role.Queries;
using CheckInSKP.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckInAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ISender _sender;

        public RolesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<IEnumerable<RoleDto>> GetRoles([FromQuery] GetRolesQuery query)
        {
            return await _sender.Send(query);
        }

        [HttpGet("{roleId}")]
        [AuthorizeByUserRole((int)RoleEnum.Admin)]
        public async Task<RoleDto> GetRoleById([FromRoute] int roleId)
        {
            var query = new GetRoleByIdQuery { RoleId = roleId };
            return await _sender.Send(query);
        }
    }
}
