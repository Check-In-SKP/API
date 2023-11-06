using CheckInSKP.Domain.Entities;
using CheckInSKP.Domain.Repositories;
using CheckInSKP.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Infrastructure.Services
{
    public class TokenValidationService : ITokenValidationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDeviceRepository _deviceRepository;

        public TokenValidationService(IUserRepository userRepository, IDeviceRepository deviceRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _deviceRepository = deviceRepository;
        }

        // Checks if the user has a valid/authorized role within a list of roles
        public async Task<bool> UserHasValidRole(int userId, params int[] roleIds)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null && roleIds.Contains(user.RoleId);
        }

        // Checks if user claims is valid           (used to check if a claim is outdated)
        public async Task<bool> ValidateUserClaims(int userId, string username, int roleClaim)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null && user.Username.Equals(username) && user.RoleId.Equals(roleClaim);
        }

        // Checks if the device is authorized       (used to check if a claim is outdated)
        public async Task<bool> DeviceIsAuthorized(Guid deviceId)
        {
            var device = await _deviceRepository.GetByIdAsync(deviceId);
            return device != null && device.IsAuthorized;
        }
    }
}
