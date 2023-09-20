using AutoMapper;
using CheckInMonitorAPI.Models.DTOs.Role;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CheckInMonitorAPI.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IUserService userService, IRoleService roleService, IMapper mapper, ILogger<RoleController> logger)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] CreateRoleDTO createRoleDTO)
        {
            if (createRoleDTO == null)
            {
                return BadRequest("Role data cannot be null");
            }
            var role = _mapper.Map<Role>(createRoleDTO);
            await _roleService.AddAsync(role);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }
    }
}
