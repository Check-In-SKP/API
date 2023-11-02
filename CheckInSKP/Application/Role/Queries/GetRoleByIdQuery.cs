using AutoMapper;
using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Role.Queries
{
    public record GetRoleByIdQuery : IRequest<RoleDto>
    {
        public int Id { get; init; }
    }

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public GetRoleByIdQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Role role = await _roleRepository.GetByIdAsync(request.Id);
            RoleDto roleDto = _mapper.Map<RoleDto>(role);
            return roleDto;
        }
    }
}
