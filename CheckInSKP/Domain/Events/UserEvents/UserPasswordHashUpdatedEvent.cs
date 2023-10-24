using CheckInSKP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Domain.Events.UserEvents
{
    public class UserPasswordHashUpdatedEvent : DomainEvent
    {
        public int UserId { get; }
        public string PasswordHash { get; }
        public UserPasswordHashUpdatedEvent(int userId, string passwordHash)
        {
            UserId = userId;
            PasswordHash = passwordHash;
        }
    }
}
