using AutoMapper;
using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.TimeType.Queries
{
    public record GetTimeTypesQuery : IRequest<IEnumerable<TimeTypeDto>>;

    public class GetTimeTypesQueryHandler : IRequestHandler<GetTimeTypesQuery, IEnumerable<TimeTypeDto>>
    {
        private readonly ITimeTypeRepository _timeTypeRepository;
        private readonly IMapper _mapper;

        public GetTimeTypesQueryHandler(ITimeTypeRepository timeTypeRepository, IMapper mapper)
        {
            _timeTypeRepository = timeTypeRepository ?? throw new ArgumentNullException(nameof(timeTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<TimeTypeDto>> Handle(GetTimeTypesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.TimeType> timeTypes = await _timeTypeRepository.GetAllAsync();
            IEnumerable<TimeTypeDto> timeTypeDtos = _mapper.Map<IEnumerable<TimeTypeDto>>(timeTypes);
            return timeTypeDtos;
        }
    }
}
