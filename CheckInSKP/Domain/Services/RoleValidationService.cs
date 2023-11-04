using CheckInSKP.Domain.Entities;
using CheckInSKP.Domain.Repositories;
using CheckInSKP.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Services
{
    public class RoleValidationService : IRoleValidationService
    {
        private readonly IUserRepository _userRepository;

        public RoleValidationService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> UserHasValidRole(int userId, int roleId)
        {
            User user = await _userRepository.GetByIdAsync(userId) ?? throw new Exception($"User with id {userId} not found");

            if(user.RoleId == roleId)
            {
                return true;
            }

            return false;
        }
    }
}
