using AutoMapper;
using CheckInMonitorAPI.Models.DTOs.TimeLog;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CheckInMonitorAPI.Controllers
{
    [Route("api/[controller]")]
    public class TimeLogsController : Controller
    {
        private readonly ITimeLogService _timeLogService;
        private readonly ILogger<TimeLogsController> _logger;
        private readonly IMapper _mapper;

        public TimeLogsController(ITimeLogService timeLogService, ILogger<TimeLogsController> logger, IMapper mapper)
        {
            _timeLogService = timeLogService ?? throw new ArgumentNullException(nameof(timeLogService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> AddTimeLog([FromBody] CreateTimeLogDTO createTimeLogDTO)
        {
            if (createTimeLogDTO == null)
            {
                return BadRequest("TimeLog data cannot be null");
            }

            var timeLog = _mapper.Map<TimeLog>(createTimeLogDTO);
            await _timeLogService.AddAsync(timeLog);
            var response = _mapper.Map<ResponseTimeLogDTO>(timeLog);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeLogById(int id)
        {
            var timeLog = _mapper.Map<ResponseTimeLogDTO>(await _timeLogService.GetByIdAsync(id));

            if (timeLog == null)
            {
                return NotFound();
            }

            return Ok(timeLog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeLog(int id, [FromBody] UpdateTimeLogDTO updateTimeLogDTO)
        {
            if (updateTimeLogDTO == null)
            {
                return BadRequest("TimeLog data cannot be null");
            }
            
            var existingTimeLog = await _timeLogService.GetByIdAsync(id);
            if (existingTimeLog == null)
            {
                return NotFound();
            }

            await _timeLogService.UpdateAsync(_mapper.Map(updateTimeLogDTO, existingTimeLog));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeLog(int id)
        {
            if (!_timeLogService.EntityExist(id))
            {
                return NotFound();
            }

            await _timeLogService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTimeLogs()
        {
            var timeLogs = await _timeLogService.GetAllAsync();
            
            if(timeLogs == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<IEnumerable<ResponseTimeLogDTO>>(timeLogs);
            return Ok(response);
        }
    }
}
