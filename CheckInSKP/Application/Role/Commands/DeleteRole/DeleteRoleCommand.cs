using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Role.Commands.DeleteRole
{
    public record DeleteRoleCommand : IRequest
    {
        public int Id { get; init; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Role role = await _roleRepository.GetByIdAsync(request.Id);
            if (role == null)
            {
                throw new Exception($"Role with id {request.Id} not found");
            }
            await _roleRepository.RemoveAsync(role.Id);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return;
        }
    }
}
