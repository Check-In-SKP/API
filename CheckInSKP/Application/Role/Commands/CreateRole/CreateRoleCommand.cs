using AutoMapper;
using CheckInSKP.Domain.Factories;
using CheckInSKP.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.Role.Commands.CreateRole
{
    public record CreateRoleCommand : IRequest<int>
    {
        public int Id { get; set; }
        public required string Name { get; init; }
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
    {
        private readonly RoleFactory _roleFactory;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandHandler(RoleFactory roleFactory, IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleFactory = roleFactory ?? throw new ArgumentNullException(nameof(roleFactory));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = _roleFactory.CreateNewRole(request.Name);

            await _roleRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync(cancellationToken);
            
            return entity.Id;
        }
    }
}
