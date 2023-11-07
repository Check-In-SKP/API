using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheckInAPI.Controllers
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


    }
}
