using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckInSKP.Domain.Factories;
using CheckInSKP.Domain.Repositories;

namespace CheckInSKP.Application.Device.Commands.CreateDevice
{
    public record CreateDeviceCommand : IRequest<Guid>
    {
        public string? Label { get; init; }
    }

    public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, Guid>
    {
        private readonly DeviceFactory _deviceFactory;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDeviceCommandHandler(DeviceFactory deviceFactory, IDeviceRepository deviceRepository, IUnitOfWork unitOfWork)
        {
            _deviceFactory = deviceFactory ?? throw new ArgumentNullException(nameof(deviceFactory));
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Guid> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            var entity = _deviceFactory.CreateNewDevice(request.Label);

            await _deviceRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return entity.Id;
        }
    }
}
