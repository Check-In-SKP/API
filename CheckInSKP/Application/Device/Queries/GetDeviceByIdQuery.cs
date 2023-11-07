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
    public record GetDeviceByIdQuery : IRequest<DeviceDto>
    {
        public Guid DeviceId { get; init; }
    }

    public class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, DeviceDto>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;
        public GetDeviceByIdQueryHandler(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<DeviceDto> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Device device = await _deviceRepository.GetByIdAsync(request.DeviceId);
            DeviceDto deviceDto = _mapper.Map<DeviceDto>(device);
            return deviceDto;
        }
    }
}
