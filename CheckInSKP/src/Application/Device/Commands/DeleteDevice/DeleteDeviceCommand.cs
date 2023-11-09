using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CheckInSKP.Application.Device.Commands.DeleteDevice
{
    public record DeleteDeviceCommand : IRequest
    {
        public Guid DeviceId { get; init; }
    }

    public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDeviceCommandHandler(IDeviceRepository deviceRepository, IUnitOfWork unitOfWork)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Device device = await _deviceRepository.GetByIdAsync(request.DeviceId) ?? throw new Exception($"Device with id {request.DeviceId} not found");
            await _deviceRepository.RemoveAsync(device.Id);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return;
        }
    }
}
