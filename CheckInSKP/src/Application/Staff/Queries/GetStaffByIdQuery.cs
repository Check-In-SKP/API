using AutoMapper;
using CheckInSKP.Application.Staff.Queries.Dtos;
using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Staff.Queries
{
    public record GetStaffByIdQuery : IRequest<StaffDto>
    {
        public Guid StaffId { get; init; }
    }

    public class GetStaffByIdQueryHandler : IRequestHandler<GetStaffByIdQuery, StaffDto>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetStaffByIdQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<StaffDto> Handle(GetStaffByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.StaffAggregate.Staff staff = await _staffRepository.GetByIdAsync(request.StaffId) ?? throw new Exception($"Staff with id {request.StaffId} not found");
            StaffDto staffDto = _mapper.Map<StaffDto>(staff);
            return staffDto;
        }
    }
}
