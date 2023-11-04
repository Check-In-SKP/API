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

        // Checks if the user has a valid/authorized role within the provided list of roles
        public async Task<bool> UserHasValidRole(int userId, params int[] roleIds)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null && roleIds.Contains(user.RoleId);
        }

        // Checks if the user role claim is valid (Useful to check if a role claim is outdated and token has yet to be revoked or expire)
        public async Task<bool> UserRoleClaimIsValid(int userId, int roleClaim)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null && user.RoleId.Equals(roleClaim);
        }
    }
}
