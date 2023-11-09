using CheckInSKP.Domain.Events.UserEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.User.EventHandlers
{
    public class UserRoleUpdatedEventHandler : INotificationHandler<UserRoleUpdatedEvent>
    {
        private readonly ILogger<UserRoleUpdatedEventHandler> _logger;

        public UserRoleUpdatedEventHandler(ILogger<UserRoleUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserRoleUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Role updated for user {UserId}", notification.UserId);

            return Task.CompletedTask;
        }
    }
}
