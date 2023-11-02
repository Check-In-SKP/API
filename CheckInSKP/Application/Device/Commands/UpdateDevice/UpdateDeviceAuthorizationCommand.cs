using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Device.Commands.UpdateDevice
{
    public record UpdateDeviceAuthorizationCommand : IRequest
    {
        public Guid DeviceId { get; init; }
        public required bool IsAuthorized { get; init; }
    }

    public class UpdateDeviceAuthorizationCommandHandler : IRequestHandler<UpdateDeviceAuthorizationCommand>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateDeviceAuthorizationCommandHandler(IDeviceRepository deviceRepository, IUnitOfWork unitOfWork)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Handle(UpdateDeviceAuthorizationCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Device device = await _deviceRepository.GetByIdAsync(request.DeviceId);

            if (device == null)
            {
                throw new Exception($"Device with id {request.DeviceId} not found");
            }

            if (request.IsAuthorized)
            {
                device.Authorize();
            }
            else
            {
                device.Deauthorize();
            }

            await _deviceRepository.UpdateAsync(device);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return;
        }
    }
}
