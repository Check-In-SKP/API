using CheckInSKP.Application.Common.Interfaces;
using CheckInSKP.Domain.Factories;
using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.User.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<int>
    {
        public required string Name { get; init; }
        public required string Username { get; init; }
        public required string Password { get; init; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly UserFactory _userFactory;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(UserFactory userFactory, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
            _roleRepository = unitOfWork.RoleRepository ?? throw new ArgumentNullException(nameof(unitOfWork.RoleRepository));
            _userRepository = unitOfWork.UserRepository ?? throw new ArgumentNullException(nameof(unitOfWork.UserRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _passwordHasher = passwordHasher;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Checks if user already exists
            if (await _userRepository.GetByUsernameAsync(request.Username) != null)
            {
                throw new Exception($"User with username {request.Username} already exists");
            }

            // Gets the default role
            Domain.Entities.Role role = await _roleRepository.GetByIdAsync(4) ?? throw new Exception($"Role not found");

            if(role.Name != "User")
            {
                throw new Exception($"Role with ID 4 is not the default role");
            }

            string passwordHash = _passwordHasher.HashPassword(request.Password);

            Domain.Entities.User user = _userFactory.CreateNewUser(request.Name, request.Username, passwordHash, role.Id);

            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return user.Id;
        }
    }
}
