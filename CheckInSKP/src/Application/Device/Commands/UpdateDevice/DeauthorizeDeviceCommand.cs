using CheckInSKP.Domain.Repositories;
using MediatR;

namespace CheckInSKP.Application.Device.Commands.UpdateDevice
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
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Handle(DeauthorizeDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByIdAsync(request.DeviceId) ?? throw new Exception($"Device with id {request.DeviceId} not found");
            device.Deauthorize();

            await _deviceRepository.UpdateAsync(device);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return;
        }
    }
}
