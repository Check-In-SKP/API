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
    public class DeviceAuthorizationService : IDeviceAuthorizationService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUserRepository _userRepository;

        public DeviceAuthorizationService(IDeviceRepository deviceRepository, IUserRepository userRepository)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task AuthorizeDeviceAsync(Guid deviceId, int userId)
        {
            // Gets the device and user
            Device device = await _deviceRepository.GetByIdAsync(deviceId) ?? throw new Exception($"Device with id {deviceId} not found");
            User user = await _userRepository.GetByIdAsync(userId) ?? throw new Exception($"User with id {userId} not found");

            // Checks if the user is admin
            if (user.RoleId != 1) throw new UnauthorizedAccessException("User must be an admin to authorize a device.");

            // Authorizes the device
            device.Authorize();

            await _deviceRepository.UpdateAsync(device);
        }

        public async Task DeauthorizeDeviceAsync(Guid deviceId, int userId)
        {
            // Gets the device and user
            Device device = await _deviceRepository.GetByIdAsync(deviceId) ?? throw new Exception($"Device with id {deviceId} not found");
            User user = await _userRepository.GetByIdAsync(userId) ?? throw new Exception($"User with id {userId} not found");

            // Checks if the user is admin
            if (user.RoleId != 1) throw new UnauthorizedAccessException("User must be an admin to deauthorize a device.");

            device.Authorize();

            await _deviceRepository.UpdateAsync(device);
        }
    }
}
