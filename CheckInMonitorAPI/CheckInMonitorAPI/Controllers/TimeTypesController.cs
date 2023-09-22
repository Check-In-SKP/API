using AutoMapper;
using CheckInMonitorAPI.Models.DTOs.TimeType;
using CheckInMonitorAPI.Models.DTOs.User;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CheckInMonitorAPI.Controllers
{
    [Route("api/[controller]")]
    public class TimeTypesController : Controller
    {
        private readonly ITimeTypeService _timeTypeService;
        private readonly IMapper _mapper;
        private readonly ILogger<TimeTypesController> _logger;

        public TimeTypesController(ITimeTypeService timeTypeService, IMapper mapper, ILogger<TimeTypesController> logger)
        {
            _timeTypeService = timeTypeService ?? throw new ArgumentNullException(nameof(timeTypeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> AddTimeType([FromBody] CreateTimeTypeDTO createTimeTypeDTO)
        {
            if (createTimeTypeDTO == null)
            {
                return BadRequest("TimeType data cannot be null");
            }
            var timeType = _mapper.Map<TimeType>(createTimeTypeDTO);
            await _timeTypeService.AddAsync(timeType);

            var response = _mapper.Map<ResponseUserDTO>(timeType);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeTypeById(int id)
        {
            var timeType = _mapper.Map<ResponseTimeTypeDTO>(await _timeTypeService.GetByIdAsync(id));

            if (timeType == null)
            {
                return NotFound();
            }

            return Ok(timeType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeType(int id, [FromBody] UpdateTimeTypeDTO updateTimeTypeDTO)
        {
            if (updateTimeTypeDTO == null)
            {
                return BadRequest("TimeType data cannot be null");
            }

            var existingTimeType = await _timeTypeService.GetByIdAsync(id);
            if (existingTimeType == null)
            {
                return NotFound();
            }

            await _timeTypeService.UpdateAsync(_mapper.Map(updateTimeTypeDTO, existingTimeType));
            return Ok(updateTimeTypeDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeType(int id)
        {
            if (!_timeTypeService.EntityExist(id))
            {
                return NotFound();
            }

            await _timeTypeService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTimeTypes()
        {
            var timeTypes = await _timeTypeService.GetAllAsync();
            if (timeTypes == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<IEnumerable<ResponseTimeTypeDTO>>(timeTypes);
            return Ok(response);
        }
    }
}
