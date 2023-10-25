using AutoMapper;
using CheckInSKP.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.TimeType.Queries
{
    public record GetTimeTypeByIdQuery : IRequest<TimeTypeDto>
    {
        public int Id { get; init; }
    }

    public class GetTimeTypeByIdQueryHandler : IRequestHandler<GetTimeTypeByIdQuery, TimeTypeDto>
    {
        private readonly ITimeTypeRepository _timeTypeRepository;
        private readonly IMapper _mapper;
        public GetTimeTypeByIdQueryHandler(ITimeTypeRepository timeTypeRepository, IMapper mapper)
        {
            _timeTypeRepository = timeTypeRepository ?? throw new ArgumentNullException(nameof(timeTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<TimeTypeDto> Handle(GetTimeTypeByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.TimeType timeType = await _timeTypeRepository.GetByIdAsync(request.Id);
            TimeTypeDto timeTypeDto = _mapper.Map<TimeTypeDto>(timeType);
            return timeTypeDto;
        }
    }
}
