using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckInAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLogsController : ControllerBase
    {
        private readonly ISender _sender;

        public TimeLogsController(ISender sender)
        {
            _sender = sender;
        }
    }
}
