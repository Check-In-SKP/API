using CheckInSKP.Domain.Events.StaffEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Staff.EventHandlers
{
    public class StaffPhoneNumberUpdatedEventHandler : INotificationHandler<StaffPhoneNumberUpdatedEvent>
    {
        private readonly ILogger<StaffPhoneNumberUpdatedEventHandler> _logger;

        public StaffPhoneNumberUpdatedEventHandler(ILogger<StaffPhoneNumberUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(StaffPhoneNumberUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Phone number updated for staff {StaffId}", notification.StaffId);

            return Task.CompletedTask;
        }
    }
}
