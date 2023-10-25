using CheckInSKP.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.Device.Commands.DeauthorizeDevice
{
    public record DeauthorizeDeviceCommand : IRequest
    {
        public Guid DeviceId { get; init; }
    }

    public class DeauthorizeDeviceCommandHandler : IRequestHandler<DeauthorizeDeviceCommand>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeauthorizeDeviceCommandHandler(IDeviceRepository deviceRepository, IUnitOfWork unitOfWork)
        {
            _deviceRepository = unitOfWork.DeviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Handle(DeauthorizeDeviceCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Device device = await _deviceRepository.GetByIdAsync(request.DeviceId);
            if (device == null)
            {
                throw new Exception($"Device with id {request.DeviceId} not found");
            }
            device.Deauthorize();
            await _deviceRepository.UpdateAsync(device);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return;
        }
    }
}
