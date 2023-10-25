using CheckInSKP.Domain.Events.DeviceEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.Device.EventHandlers
{
    public class DeviceAuthorizedEventHandler : INotificationHandler<DeviceAuthorizedEvent>
    {
        private readonly ILogger<DeviceAuthorizedEventHandler> _logger;

        public DeviceAuthorizedEventHandler(ILogger<DeviceAuthorizedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DeviceAuthorizedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Device {DeviceId} authorized", notification.DeviceId);
            return Task.CompletedTask;
        }
    }

}
