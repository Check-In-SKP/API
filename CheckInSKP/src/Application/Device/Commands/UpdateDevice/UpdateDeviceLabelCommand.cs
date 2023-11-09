using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Device.Commands.UpdateDevice
{
    public record UpdateDeviceLabelCommand : IRequest
    {
        public Guid DeviceId { get; init; }
        public required string Label { get; init; }
    }

    public class UpdateDeviceLabelCommandHandler : IRequestHandler<UpdateDeviceLabelCommand>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDeviceLabelCommandHandler(IDeviceRepository deviceRepository, IUnitOfWork unitOfWork)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Handle(UpdateDeviceLabelCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Device device = await _deviceRepository.GetByIdAsync(request.DeviceId) ?? throw new Exception($"Device with id {request.DeviceId} not found");
            device.UpdateLabel(request.Label);

            await _deviceRepository.UpdateAsync(device);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return;
        }
    }
}
