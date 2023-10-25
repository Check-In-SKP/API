using CheckInSKP.Domain.Factories;
using CheckInSKP.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.User.Commands.CreateUserToken
{
    public record CreateUserTokenCommand : IRequest<string>
    {
        public Guid DeviceId { get; init; }
        public required string Username { get; init; }
        public required string PasswordHash { get; init; }
    }

    public class CreateUserTokenCommandHandler : IRequestHandler<CreateUserTokenCommand, string>
    {
        private readonly UserFactory _userFactory;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserTokenCommandHandler(UserFactory userFactory, IDeviceRepository deviceRepository, IUnitOfWork unitOfWork)
        {
            _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
            _userRepository = unitOfWork.UserRepository ?? throw new ArgumentNullException(nameof(unitOfWork.UserRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<string> Handle(CreateUserTokenCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.UserAggregate.User user = await _userRepository.GetByUsernameAsync(request.Username) ?? throw new Exception($"User with username {request.Username} not found");

            if (!user.ValidatePasswordHash(request.PasswordHash))
            {
                throw new Exception($"Password is invalid");
            }

            Domain.Entities.Device device = await _deviceRepository.GetByIdAsync(request.DeviceId) ?? throw new Exception($"Device with id {request.DeviceId} not found");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TestKey"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.SerialNumber, device.Id.ToString()),
                new Claim(ClaimTypes.Hash, user.PasswordHash)
            };

            return null;
        }
    }
}
