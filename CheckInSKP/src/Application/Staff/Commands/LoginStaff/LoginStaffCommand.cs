using CheckInSKP.Application.Common.Interfaces;
using CheckInSKP.Domain.Enums;
using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Staff.Commands.LoginStaff
{
    public class LoginStaffCommand : IRequest<string>
    {
        public required Guid DeviceId { get; init; }
        public required string CardNumber { get; init; }
        public required string Password { get; init; }
    }

    public class LoginStaffCommandHandler : IRequestHandler<LoginStaffCommand, string>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public LoginStaffCommandHandler(IDeviceRepository deviceRepository, IStaffRepository staffRepository, IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<string> Handle(LoginStaffCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByIdAsync(request.DeviceId) ?? throw new Exception($"Device with id {request.DeviceId} not found");
            var staff = await _staffRepository.GetByCardNumberAsync(request.CardNumber) ?? throw new Exception($"Staff with card number {request.CardNumber} not found");
            var user = await _userRepository.GetByIdAsync(staff.UserId) ?? throw new Exception($"User with id {staff.UserId} not found");

            if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
                throw new Exception("Invalid password");

            // Authorize the device if the user is an admin
            if (user.RoleId == (int)RoleEnum.Admin && !device.IsAuthorized)
            {
                device.Authorize();
                await _deviceRepository.UpdateAsync(device);
            }

            if (!device.IsAuthorized)
                throw new Exception($"Device with id {request.DeviceId} is not authorized");

            var token = _tokenService.GenerateAccessToken(user, device) ?? throw new Exception("Token generation failed");
            await _unitOfWork.CompleteAsync(cancellationToken);
            return token;
        }
    }
}
