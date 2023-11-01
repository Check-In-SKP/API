using CheckInSKP.Domain.Factories;
using CheckInSKP.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        public CreateUserTokenCommandHandler(UserFactory userFactory, IDeviceRepository deviceRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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

            // TODO make this better

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TestKey"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.SerialNumber, device.Id.ToString()),
                new Claim(ClaimTypes.Hash, user.PasswordHash)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            var tokenEntity = _userFactory.CreateNewToken(token, DateTime.UtcNow.AddHours(1));
            user.AddToken(tokenEntity);

            await _unitOfWork.CompleteAsync(cancellationToken);

            return token;
        }
    }
}
