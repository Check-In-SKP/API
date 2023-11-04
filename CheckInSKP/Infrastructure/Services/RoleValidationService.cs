using CheckInSKP.Application.Common.Interfaces;
using CheckInSKP.Domain.Entities;
using CheckInSKP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Infrastructure.Services
{
    public class RoleValidationService : IRoleValidationService
    {
        private readonly IUserRepository _userRepository;

        public RoleValidationService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> UserHasValidRole(int userId, params int[] roleIds)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null && roleIds.Contains(user.RoleId);
        }
    }
}
