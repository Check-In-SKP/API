using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.User.Commands.UpdateUser
{
    public record UpdateUserCommand : IRequest
    {
        public int UserId { get; init; }
        public required string Name { get; init; }
        public required string Username { get; init; }
        public required string PasswordHash { get; init; }
        public int RoleId { get; init; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateUserCommandHandler(IRoleRepository roleRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.User user = await _userRepository.GetByIdAsync(request.UserId) ?? throw new Exception($"User with id {request.UserId} not found");

            if (!await _roleRepository.ExistsAsync(request.RoleId))
            {
                throw new Exception($"Role with id {request.RoleId} not found");
            }

            user.UpdateName(request.Name);
            user.UpdateUsername(request.Username);
            user.UpdatePasswordHash(request.PasswordHash);
            user.UpdateRole(request.RoleId);

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return;
        }
    }
}
