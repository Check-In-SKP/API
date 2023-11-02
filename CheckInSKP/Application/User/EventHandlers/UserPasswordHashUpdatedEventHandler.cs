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
    public class UserPasswordHashUpdatedEventHandler : INotificationHandler<UserPasswordHashUpdatedEvent>
    {
        private readonly ILogger<UserPasswordHashUpdatedEventHandler> _logger;

        public UserPasswordHashUpdatedEventHandler(ILogger<UserPasswordHashUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserPasswordHashUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Password hash updated for user {UserId}", notification.UserId);

            return Task.CompletedTask;
        }
    }
}
