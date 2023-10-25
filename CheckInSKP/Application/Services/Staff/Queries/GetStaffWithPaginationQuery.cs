using AutoMapper;
using CheckInSKP.Application.Services.Staff.Queries.Dtos;
using CheckInSKP.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.Staff.Queries
{
    public record GetStaffWithPaginationQuery : IRequest<IEnumerable<StaffDto>>
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
    }

    public class GetStaffWithPaginationQueryHandler : IRequestHandler<GetStaffWithPaginationQuery, IEnumerable<StaffDto>>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;
        public GetStaffWithPaginationQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<StaffDto>> Handle(GetStaffWithPaginationQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.StaffAggregate.Staff> staffs = await _staffRepository.GetAllWithPaginationAsync(request.Page, request.PageSize);
            IEnumerable<StaffDto> staffDtos = _mapper.Map<IEnumerable<StaffDto>>(staffs);

            return staffDtos;
        }
    }
}
