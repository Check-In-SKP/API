using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.User.Commands.UpdateUser
{
    public record UpdateUserRoleCommand : IRequest
    {
        public required Guid UserId { get; init; }
        public required int RoleId { get; init; }
    }

    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserRoleCommandHandler(IRoleRepository roleRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            if (!await _roleRepository.ExistsAsync(request.RoleId))
            {
                throw new Exception($"Role with id {request.RoleId} not found");
            }

            Domain.Entities.User user = await _userRepository.GetByIdAsync(request.UserId) ?? throw new Exception($"User with id {request.UserId} not found");
            user.UpdateRole(request.RoleId);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return;
        }
    }
}
