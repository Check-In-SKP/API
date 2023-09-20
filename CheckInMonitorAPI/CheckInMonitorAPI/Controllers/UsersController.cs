using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CheckInMonitorAPI.Models.DTOs;
using CheckInMonitorAPI.Models.DTOs.User;
using AutoMapper;

namespace CheckInMonitorAPI.Controllers
{

    //NOTE TO SELF: Add exceptions, logging, and validation

    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, IRoleService roleService, IMapper mapper, ILogger<UsersController> logger)
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
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("User data cannot be null");
            }

            var existingUser = await _userService.GetByIdAsync(userDTO.Id);
            
            if (existingUser == null)
            {
                return NotFound();
            }

            _mapper.Map(userDTO, existingUser);
            await _userService.UpdateAsync(existingUser);

            return Ok(userDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!_userService.EntityExist(id))
            {
                return NotFound();
            }
            await _userService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            if(users == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<IEnumerable<ResponseUserDTO>>(users);
            return Ok(response);
        }


        // Yet to be implemented                                                        <----------

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest("User data cannot be null");
            }
            //var user = await _userService.Login(loginDTO.Username, loginDTO.Password);
            //if (user == null)
            //{
            //    return NotFound();
            //}
            //var response = _mapper.Map<ResponseUserDTO>(user);
            return Ok();
        }
    }
}
