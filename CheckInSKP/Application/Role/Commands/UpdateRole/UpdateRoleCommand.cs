using AutoMapper.Features;
using CheckInSKP.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.Role.Commands.UpdateRole
{
    public record UpdateRoleCommand : IRequest
    {
        public int Id { get; init; }
        public required string Name { get; init; }
    }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Role role = await _roleRepository.GetByIdAsync(request.Id);
            if (role == null)
            {
                throw new Exception($"Role with id {request.Id} not found");
            }
            role.UpdateName(request.Name);
            await _roleRepository.UpdateAsync(role);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return;
        }
    }
}
