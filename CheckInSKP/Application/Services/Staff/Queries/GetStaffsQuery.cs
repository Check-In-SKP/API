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
    public record GetStaffsQuery : IRequest<List<StaffDto>>;

    public class GetStaffsQueryHandler : IRequestHandler<GetStaffsQuery, List<StaffDto>>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;

        public GetStaffsQueryHandler(IStaffRepository staffRepository, IMapper mapper)
        {
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<StaffDto>> Handle(GetStaffsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.StaffAggregate.Staff> staffs = await _staffRepository.GetAllAsync();
            List<StaffDto> staffDtos = _mapper.Map<List<StaffDto>>(staffs);
            return staffDtos;
        }
    }
}
