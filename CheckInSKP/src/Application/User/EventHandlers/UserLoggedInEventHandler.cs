using CheckInSKP.Application.Device.Commands.UpdateDevice;
using CheckInSKP.Domain.Enums;
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
    public class UserLoggedInEventHandler : INotificationHandler<UserLoggedInEvent>
    {
        private readonly ILogger<UserLoggedInEventHandler> _logger;

        public UserLoggedInEventHandler(ILogger<UserLoggedInEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserLoggedInEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User {UserId} logged in", notification.UserId);

            if(notification.RoleId == (int)RoleEnum.Admin)
            {
                _logger.LogInformation("User {UserId} is an admin", notification.UserId);
            }

            return Task.CompletedTask;
        }
    }
}
