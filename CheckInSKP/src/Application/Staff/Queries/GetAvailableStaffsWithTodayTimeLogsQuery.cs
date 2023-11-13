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
    public record GetAvailableStaffsWithTodayTimeLogsQuery : IRequest<IEnumerable<StaffDto>>;

    public class GetAvailableStaffWithTodayTimeLogsQueryHandler : IRequestHandler<GetAvailableStaffsWithTodayTimeLogsQuery, IEnumerable<StaffDto>>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetAvailableStaffWithTodayTimeLogsQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<StaffDto>> Handle(GetAvailableStaffsWithTodayTimeLogsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.StaffAggregate.Staff?> staffs = await _staffRepository.GetAvailableStaffsWithTodayTimeLogs();
            IEnumerable<StaffDto> staffDtos = _mapper.Map<IEnumerable<StaffDto>>(staffs);
            return staffDtos;
        }
    }
}
