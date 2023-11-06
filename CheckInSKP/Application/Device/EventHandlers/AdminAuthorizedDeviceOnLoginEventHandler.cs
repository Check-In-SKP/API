using CheckInSKP.Application.Device.Commands.UpdateDevice;
using CheckInSKP.Domain.Entities;
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
    public class AdminAuthorizedDeviceOnLoginEventHandler : INotificationHandler<AdminAuthorizedDeviceOnLoginEvent>
    {
        private readonly ILogger<AdminAuthorizedDeviceOnLoginEventHandler> _logger;
        private readonly IMediator _mediator;

        public AdminAuthorizedDeviceOnLoginEventHandler(ILogger<AdminAuthorizedDeviceOnLoginEventHandler> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Handle(AdminAuthorizedDeviceOnLoginEvent notification, CancellationToken cancellationToken)
        {
            // Authorizes device
            await _mediator.Send(new AuthorizeDeviceCommand { DeviceId = notification.DeviceId }, cancellationToken);
            _logger.LogInformation("Admin authorized device {DeviceId} on login", notification.DeviceId);
            
            return;
        }
    }
}
