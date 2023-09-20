using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CheckInMonitorAPI.Models.DTOs;
using CheckInMonitorAPI.Models.DTOs.User;
using AutoMapper;

namespace CheckInMonitorAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IRoleService roleService, IMapper mapper, ILogger<UserController> logger)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDTO createUserDTO)
        {
            if (createUserDTO == null)
            {
                return BadRequest("User data cannot be null");
            }
            var user = _mapper.Map<User>(createUserDTO);
            await _userService.AddAsync(user);

            var response = _mapper.Map<ResponseUserDTO>(user);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = _mapper.Map<ResponseUserDTO>(await _userService.GetByIdAsync(id));

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
        {
            var existingUser = await _userService.GetByIdAsync(updateUserDTO.Id);
            if (updateUserDTO == null)
            {
                return BadRequest("User data cannot be null");
            }
            if (existingUser == null)
            {
                return NotFound();
            }

            _mapper.Map(updateUserDTO, existingUser);
            await _userService.UpdateAsync(existingUser);

            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_userService.EntityExist(id))
            {
                return NotFound();
            }
            await _userService.DeleteAsync(id);
            return Ok();
        }
    }
}
