using AutoMapper;
using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Device.Queries
{
    public record GetDevicesWithPaginationQuery : IRequest<IEnumerable<DeviceDto>>
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
    }

    public class GetDevicesWithPaginationQueryHandler : IRequestHandler<GetDevicesWithPaginationQuery, IEnumerable<DeviceDto>>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;
        public GetDevicesWithPaginationQueryHandler(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<DeviceDto>> Handle(GetDevicesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.Device> devices = await _deviceRepository.GetWithPaginationAsync(request.Page, request.PageSize);
            IEnumerable<DeviceDto> deviceDtos = _mapper.Map<IEnumerable<DeviceDto>>(devices);

            return deviceDtos;
        }
    }
}
