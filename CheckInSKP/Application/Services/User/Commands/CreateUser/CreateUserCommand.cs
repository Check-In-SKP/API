using CheckInSKP.Domain.Factories;
using CheckInSKP.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.User.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<int>
    {
        public required string Name { get; init; }
        public required string Username { get; init; }
        public required string PasswordHash { get; init; }
        public required int RoleId { get; init; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly UserFactory _userFactory;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(UserFactory userFactory, IUnitOfWork unitOfWork)
        {
            _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
            _roleRepository = unitOfWork.RoleRepository ?? throw new ArgumentNullException(nameof(unitOfWork.RoleRepository));
            _userRepository = unitOfWork.UserRepository ?? throw new ArgumentNullException(nameof(unitOfWork.UserRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if(!await _roleRepository.ExistsAsync(request.RoleId))
            {
                throw new Exception($"Role with id {request.RoleId} not found");
            }

            Domain.Entities.UserAggregate.User user = _userFactory.CreateNewUser(request.Name, request.Username, request.PasswordHash, request.RoleId);

            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return user.Id;
        }
    }
}
