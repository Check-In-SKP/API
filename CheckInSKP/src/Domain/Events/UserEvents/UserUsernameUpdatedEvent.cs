using CheckInSKP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.UserEvents
{
    public class UserUsernameUpdatedEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string Username { get; }
        public UserUsernameUpdatedEvent(Guid userId, string username)
        {
            UserId = userId;
            Username = username;
        }
    }
}
