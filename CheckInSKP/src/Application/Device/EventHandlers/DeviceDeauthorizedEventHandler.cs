using CheckInSKP.Domain.Events.DeviceEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Device.EventHandlers
{
    public class DeviceDeauthorizedEventHandler : INotificationHandler<DeviceDeauthorizedEvent>
    {
        private readonly ILogger<DeviceDeauthorizedEventHandler> _logger;

        public DeviceDeauthorizedEventHandler(ILogger<DeviceDeauthorizedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DeviceDeauthorizedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Device {DeviceId} deauthorized", notification.DeviceId);
            return Task.CompletedTask;
        }
    }
}
