using CheckInSKP.Domain.Events.UserEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.Services.User.EventHandlers
{
    public class UserUsernameUpdatedEventHandler : INotificationHandler<UserUsernameUpdatedEvent>
    {
        private readonly ILogger<UserUsernameUpdatedEventHandler> _logger;

        public UserUsernameUpdatedEventHandler(ILogger<UserUsernameUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserUsernameUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Username updated for user {UserId}", notification.UserId);

            return Task.CompletedTask;
        }
    }
}
